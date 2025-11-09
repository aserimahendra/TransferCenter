// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

import './modalLayout.js';

// US date display helper for inputs with class `date-us`
// Shows MM/DD/YYYY in text mode, switches to native date picker on focus,
// and posts ISO (YYYY-MM-DD) on submit for reliable server binding.
(function () {
	function pad(n) { return n.toString().padStart(2, '0'); }
	function toIso(us) {
		if (!us) return '';
		const m = us.match(/^(\d{1,2})\/(\d{1,2})\/(\d{4})$/);
		if (!m) return '';
		const mm = pad(m[1]);
		const dd = pad(m[2]);
		return `${m[3]}-${mm}-${dd}`;
	}
	function toUs(iso) {
		if (!iso) return '';
		const m = iso.match(/^(\d{4})-(\d{2})-(\d{2})$/);
		if (!m) return '';
		return `${m[2]}/${m[3]}/${m[1]}`;
	}

	function initUsDate(input) {
		if (!input || input.dataset.usDateBound === '1') return;
		input.dataset.usDateBound = '1';

		// Normalize initial value if server rendered ISO
		const v = (input.value || '').trim();
		if (v && /^\d{4}-\d{2}-\d{2}$/.test(v)) {
			input.value = toUs(v);
			input.type = 'text';
		}

		// If Bootstrap Datepicker is available, prefer it for a better UI
		if (window.jQuery && typeof window.jQuery.fn?.datepicker === 'function') {
			const $ = window.jQuery;
			const $input = $(input);
			$input.datepicker({
				format: 'mm/dd/yyyy',
				autoclose: true,
				todayHighlight: true,
				orientation: 'bottom auto',
				container: 'body'
			}).on('changeDate', function (e) {
				// Ensure value is strictly MM/DD/YYYY
				const date = e.date;
				if (date instanceof Date && !isNaN(date.getTime())) {
					const mm = pad(date.getMonth() + 1);
					const dd = pad(date.getDate());
					const yyyy = date.getFullYear();
					this.value = `${mm}/${dd}/${yyyy}`;
				}
			});
			// Showing the picker on focus is handled centrally via the document focusin listener
		} else {
			// Debug aid: surface once if plugin is missing
			if (!window.__dpWarned) {
				try { console.warn('Bootstrap Datepicker plugin not found; falling back to native picker.'); } catch (_c) {}
				window.__dpWarned = true;
			}
			// Fallback to native temporary picker if plugin is not present
			input.addEventListener('focus', () => {
				if (input.type === 'text') {
					const iso = toIso((input.value || '').trim());
					const tmp = document.createElement('input');
					tmp.type = 'date';
					tmp.style.position = 'absolute';
					tmp.style.left = '-10000px';
					tmp.style.top = '-10000px';
					tmp.value = iso || '';
					document.body.appendChild(tmp);
					const cleanup = () => { try { tmp.remove(); } catch (_) {} };
					const onChange = () => {
						const chosen = (tmp.value || '').trim();
						input.value = chosen ? toUs(chosen) : '';
						cleanup();
					};
					tmp.addEventListener('change', onChange, { once: true });
					tmp.addEventListener('input', onChange, { once: true });
					if (typeof tmp.showPicker === 'function') {
						try { tmp.showPicker(); } catch (_) { tmp.focus(); }
					} else { tmp.focus(); }
					setTimeout(cleanup, 30000);
				}
			});
			// Blur: switch back to text and show US format if user managed to switch type
			input.addEventListener('blur', () => {
				if (input.type === 'date') {
					const isoVal = (input.value || '').trim();
					input.type = 'text';
					input.value = isoVal ? toUs(isoVal) : '';
				}
			});
		}

		// If the input lives inside a form, ensure ISO is posted on submit
		const form = input.closest('form');
		if (form && form.dataset.usDateSubmitBound !== '1') {
			form.addEventListener('submit', () => {
				const inputs = form.querySelectorAll('input.date-us');
				inputs.forEach(el => {
					if (!el) return;
					if (el.type === 'text') {
						const iso = toIso((el.value || '').trim());
						el.type = 'date';
						el.value = iso || '';
					}
				});
			});
			form.dataset.usDateSubmitBound = '1';
		}
	}

	function bindUsDates(root = document) {
		root.querySelectorAll('input.date-us').forEach(initUsDate);
	}

	// Initial bind
	if (document.readyState === 'loading') {
		document.addEventListener('DOMContentLoaded', () => bindUsDates());
	} else {
		bindUsDates();
	}

	// Re-bind when modal content is reloaded into the DOM via our modal manager
	window.addEventListener('modal:list:reloaded', () => bindUsDates());

	// Defensive: ensure datepicker shows on focus for dynamically added inputs
	document.addEventListener('focusin', (e) => {
		const el = e.target && e.target.closest && e.target.closest('input.date-us');
		if (!el) return;
		if (!el.dataset.usDateBound) {
			initUsDate(el);
		}
		if (window.jQuery && typeof window.jQuery.fn?.datepicker === 'function') {
			try { window.jQuery(el).datepicker('show'); } catch (_) {}
		}
	});
})();

