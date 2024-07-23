const url = 'https://localhost:5000/api';



// Init When Load page
const initPage = async () => {
    //document.getElementById('homeActive').className = "active";

    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);


// ---------------------------------- Call API ----------------------------------
async function getCourses() {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/courses`);
}

async function getCategories() {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/Categories`);
}

async function getTotalAmountProject(id) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/projects/${id}/amount`);
}


// -------------------------------------------------------------------------------

async function pushDataOnLoad() {
	const container = document.getElementById("top6courses"); console.log("abc")
	getCourses().then(async (projects) => {
		projects = projects.slice(0, 6);
		let html = await Promise.all(projects.map(async (project) => {

			return `<div class="col-lg-4 course_col mb-5">
						<div class="course">
							<div class="course_image"><img style="width: 100%;" src="${project.image}" alt=""></div>
							<div class="course_body">
								<h3 class="course_title"><a href="/Courses/Detail?id=${project.courseId}">${project.name}</a></h3>
								<div class="course_teacher">Mr. John Taylor</div>
								<div class="course_text">
									<p>Lorem ipsum dolor sit amet, consectetur adipi elitsed do eiusmod tempor</p>
								</div>
							</div>
							<div class="course_footer">
								<div class="course_footer_content d-flex flex-row align-items-center justify-content-start">
									<div class="course_info">
										<i class="fa fa-graduation-cap" aria-hidden="true"></i>
										<span>20 đang học</span>
									</div>
									<div class="course_info">
										<i class="fa fa-star" aria-hidden="true"></i>
										<span>5 đánh giá</span>
									</div>
									<div class="text-danger course_price ml-auto">${project.price} VND</div>
								</div>
							</div>
						</div>
					</div>`
		}));
		container.innerHTML += html.join('');
	});

	getCategories().then(async (projects) => {
		const courseSelectCreate = document.getElementById('categories');

		projects.forEach(category => {
			const option = document.createElement('option');
			option.value = category.categoryId;
			option.textContent = category.name;
			courseSelectCreate.appendChild(option);
		});
	});
}


$(document).on('click', '#search', function () {
	var courseName = $("#courseName").val();
	var categories = $("#categories").val();

	$.ajax({
		url: `https://localhost:5000/api/Dashboard/getStatistic?year=${year}` + `&courseId=${courseId}`,
		type: "get",
		headers: {
			"Authorization": "Bearer " + token,
		},
		contentType: "application/json",
		success: function (result, status, xhr) {
			renderStatistic(result.statistic)
		},
		error: function (xhr, status, error) {
			console.log(xhr)
		}
	});
})
