'use strict';

(function () {
    // Ensure a global namespace for modal utilities exists as early as possible
    try {
        window.modalActions = window.modalActions || {};
    } catch (_e) {
        // no-op
    }
    if (window.__modalListBootstrap) {
        return;
    }

    window.__modalListBootstrap = true;

    const DEFAULT_SUCCESS_EVENT = 'modalForm:result';
    const LEGACY_SUCCESS_EVENT = 'modalForm:success';
    const DEFAULT_CANCEL_EVENT = 'modalForm:cancel';
    const CANCEL_EVENT_TYPE = DEFAULT_CANCEL_EVENT;

    function byId(id) {
        if (!id) {
            return null;
        }
        return document.getElementById(id);
    }

    function toBoolean(value, defaultValue) {
        if (value == null) {
            return defaultValue;
        }
        if (value === 'false' || value === '0') {
            return false;
        }
        if (value === 'true' || value === '1') {
            return true;
        }
        return defaultValue;
    }

    class ModalListManager {
        constructor(root) {
            this.root = root;
            this.listUrl = root.getAttribute('data-list-url') || '';
            this.loader = byId(root.getAttribute('data-loader-id'));
            this.alert = byId(root.getAttribute('data-alert-id'));
            this.alertText = byId(root.getAttribute('data-alert-text-id'));
            this.modalElement = byId(root.getAttribute('data-modal-id'));
            this.modalTitle = byId(root.getAttribute('data-modal-title-id'));
            this.modalFrame = byId(root.getAttribute('data-modal-frame-id'));
            this.successEvent = root.getAttribute('data-success-event') || DEFAULT_SUCCESS_EVENT;
            this.cancelEvent = root.getAttribute('data-cancel-event') || DEFAULT_CANCEL_EVENT;
            this.defaultSuccessMessage = root.getAttribute('data-success-message') || 'Changes saved successfully.';
            this.autoLoad = toBoolean(root.getAttribute('data-load-on-init'), true);
            this.tableContainer = root.querySelector('[data-list-container]');

            this.modal = this.modalElement && typeof bootstrap !== 'undefined'
                ? bootstrap.Modal.getOrCreateInstance(this.modalElement)
                : null;

            this.frameLoaded = false;
            this.alertHideHandle = null;
            this.pendingSuccessMessage = '';

            this.boundHandleTriggerClick = this.handleTriggerClick.bind(this);
            this.boundHandleMessage = this.handleMessage.bind(this);
            this.boundHandleModalHidden = this.handleModalHidden.bind(this);
        }

        init() {
            if (!this.root) {
                return;
            }

            this.root.addEventListener('click', this.boundHandleTriggerClick);
            window.addEventListener('message', this.boundHandleMessage);

            if (this.modalElement) {
                this.modalElement.addEventListener('hidden.bs.modal', this.boundHandleModalHidden);
            }

            const closeButton = this.alert ? this.alert.querySelector('.btn-close') : null;
            if (closeButton) {
                closeButton.addEventListener('click', () => {
                    if (this.alertHideHandle) {
                        window.clearTimeout(this.alertHideHandle);
                        this.alertHideHandle = null;
                    }
                    this.hideAlert();
                });
            }

            if (this.modalFrame) {
                this.modalFrame.addEventListener('load', () => {
                    this.frameLoaded = true;
                    this.hideLoader();
                });
            }

            this.wireDeleteConfirm(this.root);
            this.initTooltips(this.root);
            this.bindFilterForms(this.root);

            if (this.autoLoad && this.listUrl) {
                this.reload();
            } else {
                this.initTooltips(this.tableContainer);
            }
        }

        showLoader() {
            if (this.loader) {
                this.loader.classList.remove('d-none');
            }
        }

        hideLoader() {
            if (this.loader) {
                this.loader.classList.add('d-none');
            }
        }

        showAlert(message, variant) {
            if (!this.alert || !this.alertText) {
                return;
            }

            const text = (message && message.trim().length > 0)
                ? message
                : this.defaultSuccessMessage;

            this.alertText.textContent = text;
            if (variant === 'error') {
                this.alert.classList.remove('alert-success');
                this.alert.classList.add('alert-danger');
            } else {
                this.alert.classList.remove('alert-danger');
                this.alert.classList.add('alert-success');
            }
            this.alert.classList.remove('d-none');
            void this.alert.offsetHeight;
            this.alert.classList.add('show');

            if (this.alertHideHandle) {
                window.clearTimeout(this.alertHideHandle);
            }

            this.alertHideHandle = window.setTimeout(() => this.hideAlert(), 4000);
        }

        hideAlert() {
            if (!this.alert) {
                return;
            }

            this.alert.classList.remove('show');
            window.setTimeout(() => {
                if (this.alert) {
                    this.alert.classList.add('d-none');
                }
            }, 200);
        }

        initTooltips(scope) {
            if (typeof bootstrap === 'undefined' || !bootstrap.Tooltip) {
                return;
            }

            const root = scope && scope.querySelectorAll ? scope : document;
            root.querySelectorAll('[data-bs-toggle="tooltip"]').forEach((element) => {
                bootstrap.Tooltip.getOrCreateInstance(element);
            });
        }

        wireDeleteConfirm(scope) {
            const root = scope && scope.querySelectorAll ? scope : document;
            root.querySelectorAll('.js-confirm-delete').forEach((form) => {
                if (form.dataset.confirmBound === 'true') {
                    return;
                }

                form.dataset.confirmBound = 'true';

                form.addEventListener('submit', (event) => {
                    event.preventDefault();
                    const message = form.getAttribute('data-confirm') || 'Are you sure you want to proceed?';
                    if (window.confirm(message)) {
                        form.submit();
                    }
                });
            });
        }

        bindFilterForms(scope) {
            const root = scope && scope.querySelectorAll ? scope : document;
            root.querySelectorAll('form[data-list-filter]')
                .forEach((form) => {
                    if (form.dataset.listFilterBound === 'true') {
                        return;
                    }

                    form.dataset.listFilterBound = 'true';

                    // Handle form reset centrally so "Reset" truly clears and reloads the list
                    form.addEventListener('reset', (event) => {
                        debugger
                        event.preventDefault();
                        try {
                            // Clear known filter fields across both lists while preserving pageSize
                            const namesToClear = ['caseManager','caseMgr','transferDateFrom','transferFrom','transferDateTo','transferTo'];
                            namesToClear.forEach((name) => {
                                const el = form.querySelector(`[name="${name}"]`);
                                if (el) {
                                    el.value = '';
                                }
                            });
                            // Ensure page resets to 1
                            let pageInput = form.querySelector('input[name="page"]');
                            if (!pageInput) {
                                pageInput = document.createElement('input');
                                pageInput.type = 'hidden';
                                pageInput.name = 'page';
                                form.appendChild(pageInput);
                            }
                            pageInput.value = '1';

                            const action = form.getAttribute('action') || this.listUrl;
                            if (!action) {
                                return;
                            }
                            const formData = new FormData(form);
                            const params = new URLSearchParams(formData);
                            const query = params.toString();
                            this.listUrl = query ? `${action.split('?')[0]}?${query}` : action.split('?')[0];
                            this.reload();
                        } catch (_e) {
                            // fallback: navigate
                            try { form.submit(); } catch (_) {}
                        }
                    });

                    form.addEventListener('submit', (event) => {
                        event.preventDefault();

                        const action = form.getAttribute('action') || this.listUrl;
                        if (!action) {
                            return;
                        }

                        const formData = new FormData(form);
                        const params = new URLSearchParams(formData);
                        const query = params.toString();
                        this.listUrl = query ? `${action.split('?')[0]}?${query}` : action.split('?')[0];
                        this.reload();
                    });
                });
        }

        handleTriggerClick(event) {
            // Direct file download handler (avoids loading downloads into the modal iframe)
            const downloadTrigger = event.target.closest('[data-download-url]');
            if (downloadTrigger) {
                const url = downloadTrigger.getAttribute('data-download-url');
                const suggestedName = downloadTrigger.getAttribute('data-download-name') || '';
                if (!url) {
                    return;
                }

                event.preventDefault();
                this.showLoader();

                const triggerDownload = async () => {
                    try {
                        const resp = await fetch(url, { credentials: 'same-origin' });
                        if (!resp.ok) throw new Error('Failed to download file.');

                        // Try to infer filename from Content-Disposition
                        const cd = resp.headers.get('Content-Disposition') || resp.headers.get('content-disposition') || '';
                        let fileName = suggestedName.trim();
                        if (!fileName) {
                            const match = cd.match(/filename\*=UTF-8''([^;]+)|filename="?([^";]+)"?/i);
                            if (match) {
                                fileName = decodeURIComponent((match[1] || match[2] || '').trim());
                            }
                        }
                        if (!fileName) {
                            // Last resort filename
                            fileName = 'download.pdf';
                        }

                        const blob = await resp.blob();
                        const href = URL.createObjectURL(blob);
                        const a = document.createElement('a');
                        a.href = href;
                        a.download = fileName;
                        document.body.appendChild(a);
                        a.click();
                        a.remove();
                        setTimeout(() => URL.revokeObjectURL(href), 1000);
                    } catch (err) {
                        console.error('Download failed:', err);
                    } finally {
                        this.hideLoader();
                    }
                };

                triggerDownload();
                return;
            }

            const trigger = event.target.closest('[data-modal-url]');
            if (trigger) {
                const url = trigger.getAttribute('data-modal-url');
                if (!url || !this.modal || !this.modalFrame) {
                    return;
                }

                event.preventDefault();

                const modalTitle = trigger.getAttribute('data-modal-title');
                if (this.modalTitle && modalTitle) {
                    this.modalTitle.textContent = modalTitle;
                }

                this.pendingSuccessMessage = trigger.getAttribute('data-success-message') || '';

                this.showLoader();
                this.frameLoaded = false;
                this.modalFrame.src = url;
                this.modal.show();
                this.active = true;
                return;
            }

            const pagerLink = event.target.closest('a[data-list-page]');
            if (pagerLink) {
                const href = pagerLink.getAttribute('href');
                if (!href) {
                    return;
                }
                event.preventDefault();
                this.listUrl = href;
                this.reload();
                return;
            }
        }

        handleModalHidden() {
            this.active = false;
            this.pendingSuccessMessage = '';
            if (this.modalFrame) {
                this.modalFrame.src = '';
            }
            this.hideLoader();
        }

        handleMessage(event) {
            const data = event.data || {};
            const isSuccessEvent = data.type === this.successEvent || data.type === DEFAULT_SUCCESS_EVENT || data.type === LEGACY_SUCCESS_EVENT;
            if (!isSuccessEvent && data.type !== this.cancelEvent) {
                return;
            }

            const origin = event.origin || window.location.origin;
            if (origin && origin !== 'null' && origin !== window.location.origin) {
                return;
            }

            if (this.modalFrame && event.source && this.modalFrame.contentWindow && event.source !== this.modalFrame.contentWindow) {
                return;
            }

            if (data.type === this.cancelEvent) {
                if (this.modal) {
                    this.modal.hide();
                }
                this.hideLoader();
                return;
            }

            let payload = data.payload;
            if (typeof payload === 'string') {
                try {
                    payload = JSON.parse(payload);
                } catch (_ignore) {
                    payload = { message: payload };
                }
            }

            const payloadObject = (typeof payload === 'object' && payload !== null) ? payload : {};
            let isSuccess;
            if (Object.prototype.hasOwnProperty.call(payloadObject, 'isSuccess')) {
                isSuccess = !!payloadObject.isSuccess;
            } else if (Object.prototype.hasOwnProperty.call(payloadObject, 'success')) {
                isSuccess = !!payloadObject.success;
            } else {
                isSuccess = true;
            }

            const statusCodeValue = payloadObject.statusCode;
            let statusCode = '';
            if (typeof statusCodeValue === 'number' && Number.isFinite(statusCodeValue)) {
                statusCode = statusCodeValue.toString();
            } else if (typeof statusCodeValue === 'string' && statusCodeValue.trim().length > 0) {
                statusCode = statusCodeValue.trim();
            } else if (typeof payloadObject.code === 'string' && payloadObject.code.trim().length > 0) {
                statusCode = payloadObject.code.trim();
            } else {
                statusCode = isSuccess ? 'SUCCESS' : 'ERROR';
            }
            const messageFromPayload = typeof payloadObject.message === 'string' && payloadObject.message.trim().length > 0
                ? payloadObject.message.trim()
                : '';
            const warnings = Array.isArray(payloadObject.warnings) ? payloadObject.warnings.filter((item) => typeof item === 'string' && item.trim().length > 0) : [];
            const errors = Array.isArray(payloadObject.errors) ? payloadObject.errors.filter((item) => typeof item === 'string' && item.trim().length > 0) : [];

            const message = messageFromPayload
                || this.pendingSuccessMessage
                || this.defaultSuccessMessage
                || statusCode
                || '';

            this.pendingSuccessMessage = '';

            if (!isSuccess) {
                const errorDetail = errors.length > 0 ? `${message} ${errors.join(' ')}` : message;
                const combinedError = (errorDetail || statusCode || '').toString().trim();
                this.hideLoader();
                this.showAlert(combinedError || statusCode, 'error');

                const statusCodeNumber = Number.parseInt(statusCode, 10);
                if (!Number.isNaN(statusCodeNumber) && statusCodeNumber >= 100 && this.modalElement && this.modalElement.classList.contains('show') && this.modal) {
                    this.modal.hide();
                }

                return;
            }

            const successMessage = (() => {
                if (warnings.length === 0) {
                    return (message || statusCode || '').toString().trim();
                }
                return `${message} ${warnings.join(' ')}`.trim();
            })();

            const doReload = () => {
                this.reload(successMessage, 'success');
                if (this.modalElement) {
                    this.modalElement.removeEventListener('hidden.bs.modal', doReload);
                }
            };

            if (this.modalElement && this.modalElement.classList.contains('show')) {
                if (this.modal) {
                    this.modalElement.addEventListener('hidden.bs.modal', doReload);
                    this.modal.hide();
                }
            } else {
                this.reload(successMessage, 'success');
            }
        }

        reload(successMessage, variant) {
            if (!this.listUrl || !this.tableContainer) {
                if (successMessage) {
                    this.showAlert(successMessage, variant);
                }
                return;
            }

            this.showLoader();

            fetch(this.listUrl, { headers: { 'X-Requested-With': 'XMLHttpRequest' } })
                .then((response) => {
                    if (!response.ok) {
                        throw new Error('Failed to load list data.');
                    }
                    return response.text();
                })
                .then((html) => {
                    this.tableContainer.innerHTML = html;
                    this.wireDeleteConfirm(this.tableContainer);
                    this.initTooltips(this.tableContainer);
                    this.bindFilterForms(this.tableContainer);
                    try { window.dispatchEvent(new Event('modal:list:reloaded')); } catch(_e) {}
                    if (successMessage) {
                        this.showAlert(successMessage, variant);
                    }
                })
                .catch((error) => {
                    console.error('ModalListManager reload failed:', error);
                    if (successMessage) {
                        this.showAlert(successMessage, 'error');
                    }
                })
                .finally(() => {
                    this.hideLoader();
                });
        }
    }

    document.addEventListener('DOMContentLoaded', () => {
        document.querySelectorAll('[data-modal-list]').forEach((element) => {
            const manager = new ModalListManager(element);
            manager.init();
        });
    });

    function postModalCancel(event) {
        if (event) {
            event.preventDefault();
        }

        const payload = { type: CANCEL_EVENT_TYPE };
        const origin = (window.location.origin && window.location.origin !== 'null')
            ? window.location.origin
            : '*';

        try {
            window.parent.postMessage(payload, origin);
        } catch (_error) {
            window.parent.postMessage(payload, '*');
        }
    }

    function handleDocumentClick(event) {
        const trigger = event.target.closest('[data-modal-dismiss]');
        if (!trigger) {
            return;
        }

        const action = trigger.getAttribute('data-modal-dismiss');
        if (action !== 'cancel') {
            return;
        }

        postModalCancel(event);
    }

    window.modalActions = window.modalActions || {};
    window.modalActions.postModalCancel = postModalCancel;
    // Global helper to trigger a file download with loader handling
    window.modalActions.downloadFile = async function(url, fileName) {
        try {
            const root = document.querySelector('[data-modal-list]');
            const loaderId = root ? root.getAttribute('data-loader-id') : null;
            const loader = loaderId ? document.getElementById(loaderId) : null;
            if (loader) loader.classList.remove('d-none');

            let usedNavigationFallback = false;
            try {
                const resp = await fetch(url, {
                    credentials: 'same-origin',
                    headers: { 'Accept': 'application/pdf' }
                });
                if (!resp.ok) throw new Error('Failed to download');
                const ct = (resp.headers.get('Content-Type') || resp.headers.get('content-type') || '').toLowerCase();
                // If server returned HTML (e.g., error page) instead of PDF, fall back to native navigation
                if (ct && !ct.includes('application/pdf') && !ct.includes('application/octet-stream')) {
                    throw new Error('Unexpected content-type: ' + ct);
                }
                // Prefer server-provided filename
                const cd = resp.headers.get('Content-Disposition') || resp.headers.get('content-disposition') || '';
                let finalName = (fileName || '').trim();
                if (!finalName) {
                    const match = cd && cd.match(/filename\*=UTF-8''([^;]+)|filename="?([^";]+)"?/i);
                    if (match) finalName = decodeURIComponent((match[1] || match[2] || '').trim());
                }
                if (!finalName) finalName = 'download.pdf';

                const blob = await resp.blob();
                const href = URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.href = href;
                a.download = finalName;
                document.body.appendChild(a);
                a.click();
                a.remove();
                setTimeout(() => URL.revokeObjectURL(href), 1000);
            } catch (err) {
                // Fallback to native navigation (lets browser handle download)
                usedNavigationFallback = true;
                window.location.href = url;
            } finally {
                if (loader) {
                    if (usedNavigationFallback) {
                        setTimeout(() => loader.classList.add('d-none'), 2000);
                    } else {
                        loader.classList.add('d-none');
                    }
                }
            }
        } catch (_e) {
            // no-op
        }
    }

    document.addEventListener('click', handleDocumentClick);
})();
