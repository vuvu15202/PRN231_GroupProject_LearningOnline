const url = 'https://localhost:5000/api/Customer';



// Init When Load page
const initPage = async () => {
    document.getElementById('homeActive').className = "active";

    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);


// ---------------------------------- Call API ----------------------------------
// Get 6 Projects from last
async function getProjectsFromLast() {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/projects/6`);
}

// Get 6 News from last
async function getNewsFromLast() {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/news/6`);
}

async function getTotalAmountProject(id) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/projects/${id}/amount`);
}


// -------------------------------------------------------------------------------

async function pushDataOnLoad() {
    const container = document.getElementById("listProjects");
    getProjectsFromLast().then(async (projects) => {
        let html = await Promise.all(projects.map(async (project) => {
            const [amountData] = await Promise.all([
                getTotalAmountProject(project.projectId)
            ])
            return `<div class="col-lg-4 course_col">
                <div class="course">
                    <div class="course_image"><img src="${project.image}" alt=""></div>
                    <div class="course_body">
                        <h3 class="course_title"><a href="/project?id=${project.projectId}">${project.title}</a></h3>
                        <div class="course_teacher">JG Organization</div>
                        <div class="course_text">
                            <p>${amountData.totalAmount.toLocaleString('vi', { style: 'currency', currency: 'VND' }) } / ${project.targetAmount.toLocaleString('vi', { style: 'currency', currency: 'VND' })} </p>
                        </div>
                    </div>
                    <div class="course_footer">
                        <div class="course_footer_content d-flex flex-row align-items-center justify-content-start">
                            <div class="course_info">
                                <i class="fa fa-graduation-cap" aria-hidden="true"></i>
                                <span>${amountData.totalDonate} donates</span>
                            </div>
                            <div class="course_info">
                                <i class="fa fa-star" aria-hidden="true"></i>
                                <span>${project.like}</span>
                            </div>
                            <div class="course_price ml-auto"></div>
                        </div>
                    </div>
                </div>
            </div>`
        }));
        container.innerHTML += html.join('');
    })
}



