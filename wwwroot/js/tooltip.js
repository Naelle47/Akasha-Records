// =====================
// Tooltip universel — armes et artefacts
// Une seule div flottante repositionnée au survol
// =====================

const tooltip = document.createElement('div');
tooltip.className = 'build-tooltip';
document.body.appendChild(tooltip);

function showTooltip(e, html) {
    tooltip.innerHTML = html;
    tooltip.classList.add('show');
    moveTooltip(e);
}

function moveTooltip(e) {
    const margin = 12;
    const rect = tooltip.getBoundingClientRect();
    let x = e.clientX + margin;
    let y = e.clientY + margin;

    if (x + rect.width > window.innerWidth) x = e.clientX - rect.width - margin;
    if (y + rect.height > window.innerHeight) y = e.clientY - rect.height - margin;

    tooltip.style.left = x + window.scrollX + 'px';
    tooltip.style.top = y + window.scrollY + 'px';
}

function hideTooltip() {
    tooltip.classList.remove('show');
}

// Armes
document.querySelectorAll('[data-weapon-passive]').forEach(el => {
    el.addEventListener('mouseenter', e => {
        const name = el.dataset.weaponName;
        const passive = el.dataset.weaponPassive;
        const stat = el.dataset.weaponStat;

        if (!passive) return;

        showTooltip(e, `
            <div class="tooltip-weapon-name">${name}</div>
            ${stat ? `<div class="tooltip-weapon-stat">${stat}</div>` : ''}
            <div class="tooltip-passive-name">${passive.split(':')[0]}</div>
            <div class="tooltip-passive-text">${passive.split(':').slice(1).join(':').trim()}</div>
        `);
    });
    el.addEventListener('mousemove', moveTooltip);
    el.addEventListener('mouseleave', hideTooltip);
});

// Artefacts
document.querySelectorAll('[data-artifact-bonus]').forEach(el => {
    el.addEventListener('mouseenter', e => {
        const name = el.dataset.artifactName;
        const bonus2pc = el.dataset.artifactBonus2;
        const bonus4pc = el.dataset.artifactBonus4;

        showTooltip(e, `
            <div class="tooltip-artifact-name">${name}</div>
            ${bonus2pc ? `
                <div class="tooltip-bonus-row">
                    <span class="tooltip-badge">2</span>
                    <span>${bonus2pc}</span>
                </div>` : ''}
            ${bonus4pc ? `
                <div class="tooltip-bonus-row">
                    <span class="tooltip-badge">4</span>
                    <span>${bonus4pc}</span>
                </div>` : ''}
        `);
    });
    el.addEventListener('mousemove', moveTooltip);
    el.addEventListener('mouseleave', hideTooltip);
});