﻿const url = 'https://localhost:5000/api/Customer';
const projectId = new URL(window.location.href).searchParams.get('id') || 1;
let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

// Init When Load pageconst initPage = () => {
const initPage = async () => {
    document.getElementById('projectsActive').className = 'active';
    document.getElementById("ProjectIdMomo").value = projectId;
    document.getElementById("ProjectIdVnPay").value = projectId;
    console.log(document.getElementById("ProjectIdVnPay").value)
    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);

// ---------------------------------- Call API ----------------------------------
async function getProjectById(id) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/project/${id}`);
}

async function getTopDonateById(id) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/project/${id}/donatesTop/5`);
}

async function getTotalAmountProject(id) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/projects/${id}/amount`);
}

// -------------------------------------------------------------------------------

async function pushDataOnLoad() {
    const container = document.getElementById('projectContainer');
    const topDonateContainer = document.getElementById('topDonate');
    const listFeatureContainer = document.getElementById('listFeature');

    getProjectById(projectId).then(async (project) => {
        const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
        let html = `<div class="course_container">
                    <div class="course_title">${project.project.title}</div>
                    <div class="course_image"><img src="${project.project.image}" alt=""></div>
                    <div class="course_tabs_container">
                        <div class="tabs d-flex flex-row align-items-center justify-content-start">
                            <div class="tab active">Hoàn cảnh</div>
                        </div>
                        <div class="tab_panels">
                            <div class="tab_panel active">
                                ${project.project.story}
                            </div>
                        </div>
                    </div>
                `;
        let featureHtml = `<div class="feature d-flex flex-row align-items-center justify-content-start">
                                    <div class="feature_title"><i class="fa fa-clock-o" aria-hidden="true"></i><span>Start:</span></div>
                                    <div class="feature_text ml-auto">${new Date(project.project.startDate).toLocaleDateString("en-US", options)}</div>
                                </div>
                                <div class="feature d-flex flex-row align-items-center justify-content-start">
                                    <div class="feature_title"><i class="fa fa-clock-o" aria-hidden="true"></i><span>End:</span></div>
                                    <div class="feature_text ml-auto">${new Date(project.project.endDate).toLocaleDateString("en-US", options)}</div>
                                </div>
                                <div class="feature d-flex flex-row align-items-center justify-content-start">
                                    <div class="feature_title"><i class="fa fa-file-text-o" aria-hidden="true"></i><span>Status:</span></div>
                                    <div class="feature_text ml-auto">${project.project.discontinued == 1 ? "Continue" : "Ended"}</div>
                                </div>
                                <div class="feature d-flex flex-row align-items-center justify-content-start">
                                    <div class="feature_title"><i class="fa fa-file-text-o" aria-hidden="true"></i><span>Total:</span></div>
                                    <div class="feature_text ml-auto">${amountData.totalAmount.toLocaleString('vi', { style: 'currency', currency: 'VND' }) }</div>
                                </div>
                                <div class="feature d-flex flex-row align-items-center justify-content-start">
                                    <div class="feature_title"><i class="fa fa-file-text-o" aria-hidden="true"></i><span>Target:</span></div>
                                    <div class="feature_text ml-auto">${project.project.targetAmount.toLocaleString('vi', { style: 'currency', currency: 'VND' }) }</div>
                                </div>`;
        container.innerHTML += html;
        listFeatureContainer.innerHTML += featureHtml;
    })

    getTopDonateById(projectId).then(donateData => {
        let html = donateData.map(donate => {
            return `<div class="latest d-flex flex-row align-items-start justify-content-start">
                            <div class="latest_image"><div style="width:30px"><img src="https://cdn-icons-png.flaticon.com/512/9131/9131478.png" style="max-width:100%; width:30px" alt=""></div></div>
                            <div class="latest_content">
                                <div class="latest_title">${donate.orderInfo}</div>
                                <p>${Number(donate.amount).toLocaleString('vi', { style: 'currency', currency: 'VND' }) }</p>
                            </div>
                        </div>`;
        });
        topDonateContainer.innerHTML += html.join('');
    })

}

