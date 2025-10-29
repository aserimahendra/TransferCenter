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

		// Focus: switch to date and normalize value to ISO
		input.addEventListener('focus', () => {
			if (input.type === 'text') {
				const iso = toIso((input.value || '').trim());
				input.type = 'date';
				if (iso) input.value = iso;
				if (typeof input.showPicker === 'function') {
					try { input.showPicker(); } catch (_) {}
				}
			}
		});

		// Blur: switch back to text and show US format
		input.addEventListener('blur', () => {
			if (input.type === 'date') {
				const isoVal = (input.value || '').trim();
				input.type = 'text';
				input.value = isoVal ? toUs(isoVal) : '';
			}
		});

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
})();
