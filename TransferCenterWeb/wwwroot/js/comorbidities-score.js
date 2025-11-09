(function(){
  // We only need a single TotalPoints field now; make logic resilient if manual override field is absent.
  const flagNames = ['None','StrokeTIA','CHF','CKD_ESRD','HTN','DiabetesOrSkinIssues','MI_Angina_CAD','COPDOrRespiratoryFailure','Ventilated','PulmonaryHTN','PulmonaryHTN_Prostacyclin','ImmunocompromisedOrHIV','PsychBackground','NonCompliantWithCare','UnableToPerformADLs','DrugOrAlcoholDependence','LymphomaLeukemiaCancer','MalnutritionObesityDigestiveDisease','UnconsciousOrALOC','LOSMoreThan2Weeks','OutOfServiceArea','DNRCodeStatus','CovidPositive','RecentSurgeryAtUCI','RecentSurgeryOutsideUCI'];

  function weight(state){
    if(state.None) return 0;
    return (state.StrokeTIA?1:0)+
      (state.CHF?1:0)+
      (state.CKD_ESRD?1:0)+
      (state.HTN?1:0)+
      (state.DiabetesOrSkinIssues?1:0)+
      (state.MI_Angina_CAD?1:0)+
      (state.COPDOrRespiratoryFailure?1:0)+
      (state.Ventilated?1:0)+
      (state.PulmonaryHTN?2:0)+
      (state.PulmonaryHTN_Prostacyclin?2:0)+
      (state.ImmunocompromisedOrHIV?1:0)+
      (state.PsychBackground?2:0)+
      (state.NonCompliantWithCare?1:0)+
      (state.UnableToPerformADLs?1:0)+
      (state.DrugOrAlcoholDependence?2:0)+
      (state.LymphomaLeukemiaCancer?1:0)+
      (state.MalnutritionObesityDigestiveDisease?1:0)+
      (state.UnconsciousOrALOC?2:0)+
      (state.LOSMoreThan2Weeks?16:0)+
      (state.OutOfServiceArea?10:0)+
      (state.DNRCodeStatus?10:0)+
      (state.CovidPositive?16:0)+
      (state.RecentSurgeryAtUCI?2:0)+
      (state.RecentSurgeryOutsideUCI?16:0);
  }

  function gatherState(container){
    const state={};
    flagNames.forEach(f=>{const el=container.querySelector('[name="ComorbiditiesAndRiskScore.'+f+'"]'); state[f]=el?el.checked:false;});
    return state;
  }

  function enforceNoneExclusivity(container){
    const noneBox = container.querySelector('[name="ComorbiditiesAndRiskScore.None"]');
    if(!noneBox) return;
    if(noneBox.checked){
      // Uncheck all others
      flagNames.filter(f=>f!=='None').forEach(f=>{
        const el=container.querySelector('[name="ComorbiditiesAndRiskScore.'+f+'"]');
        if(el) el.checked = false;
      });
    }
  }

  function update(container){
    let calcInput = container.querySelector('[data-role="calculated-points"]');
    if(!calcInput){
      // Fallback: find by name if data-role wasn't rendered as expected
      calcInput = container.querySelector('[name="ComorbiditiesAndRiskScore.TotalPoints"]');
    }
    const status = container.querySelector('[data-role="points-status"]');
    if(!calcInput) return;
    enforceNoneExclusivity(container);
    const value = weight(gatherState(container));
    calcInput.value = value;
    if(status){ status.textContent = '(auto)'; }
  }

  function handleEvent(e){
    const t = e.target;
    if(!(t instanceof HTMLElement)) return;
    if(t.matches('input[type="checkbox"][name^="ComorbiditiesAndRiskScore."]')){
      const container = t.closest('table') || document;
      update(container);
    }
  }

  // Delegate to document so it works for dynamically inserted modals too
  document.addEventListener('change', handleEvent, true);
  document.addEventListener('click', handleEvent, true);
  document.addEventListener('input', handleEvent, true);

  // Try an initial pass on existing forms (non-blocking)
  if(document.readyState==='loading'){
    document.addEventListener('DOMContentLoaded', ()=>{
      const any = document.querySelector('input[type="checkbox"][name^="ComorbiditiesAndRiskScore."]');
      if(any){ update(any.closest('table')||document); }
    });
  } else {
    const any = document.querySelector('input[type="checkbox"][name^="ComorbiditiesAndRiskScore."]');
    if(any){ update(any.closest('table')||document); }
  }
})();