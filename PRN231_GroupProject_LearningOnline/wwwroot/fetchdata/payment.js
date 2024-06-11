const url = 'https://localhost:5000/api';
const courseId = new URL(window.location.href).searchParams.get('courseId') || 1;
let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

// Init When Load pageconst initPage = () => {
const initPage = async () => {
    document.getElementById('projectsActive').className = 'active';
    document.getElementById("courseIdMomo").value = courseId;
    document.getElementById("courseIdVnPay").value = courseId;
    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);

// ---------------------------------- Call API ----------------------------------
async function getCourseById(id) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/courses/${id}`);
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

    getCourseById(courseId).then(async (course) => {
        //const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
        let html = `<div class="course_container">
                    <div class="course_title">${course.name}</div>
                    <div class="course_image"><img src="https://localhost:5000/${course.image}" alt=""></div>
                    <div class="course_tabs_container">
                        <div class="tabs d-flex flex-row align-items-center justify-content-start">
								<div class="tab active">Description</div>
								<div class="tab">Review</div>
							</div>
                        <div class="tab_panels">
                            <div class="tab_panel active">
                                ${course.description}
                            </div>
                            <div class="tab_panel tab_panel-2">
                                ${course.description}
                            </div>
                        </div>

                    </div>
                `;
        document.getElementById('course-price').innerHTML = course.price + ' VND';
        container.innerHTML += html;
        initTabs();
    })
    function initTabs() {
        if ($('.tab').length) {
            $('.tab').on('click', function () {
                $('.tab').removeClass('active');
                $(this).addClass('active');
                var clickedIndex = $('.tab').index(this);

                var panels = $('.tab_panel');
                panels.removeClass('active');
                $(panels[clickedIndex]).addClass('active');
            });
        }
    }
    //getTopDonateById(courseId).then(donateData => {
    //    let html = donateData.map(donate => {
    //        return `<div class="latest d-flex flex-row align-items-start justify-content-start">
    //                        <div class="latest_image"><div style="width:30px"><img src="https://cdn-icons-png.flaticon.com/512/9131/9131478.png" style="max-width:100%; width:30px" alt=""></div></div>
    //                        <div class="latest_content">
    //                            <div class="latest_title">${donate.orderInfo}</div>
    //                            <p>${Number(donate.amount).toLocaleString('vi', { style: 'currency', currency: 'VND' }) }</p>
    //                        </div>
    //                    </div>`;
    //    });
    //    topDonateContainer.innerHTML += html.join('');
    //})

}

