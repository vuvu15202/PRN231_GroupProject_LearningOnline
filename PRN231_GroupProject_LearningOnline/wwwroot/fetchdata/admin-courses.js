const urll = 'https://localhost:5000/api';

//let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

// Init When Load pageconst initPage = () => {
const initPage = async () => {
    //document.getElementById('projectsActive').className = 'active';
    //document.getElementById("ProjectIdMomo").value = projectId;
    //document.getElementById("ProjectIdVnPay").value = projectId;
    //console.log(document.getElementById("ProjectIdVnPay").value)
    await pushDataOnLoad();
}
document.addEventListener("DOMContentLoaded", initPage);

// ---------------------------------- Call API ----------------------------------
async function getAllCourses() {
    const callApi = async (urll) => {
        return (await fetch(urll)).json();
    }
    return await callApi(`${urll}/Courses`);
}

async function getAllCatogory() {
    const callApi = async (urll) => {
        return (await fetch(urll)).json();
    }
    return await callApi(`${urll}/Categories`);
}

// -------------------------------------------------------------------------------
let coursesGlobal = [];
let categoriesGlobal = [];

async function pushDataOnLoad() {


    getAllCourses().then(async (courses) => {
        //const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
        coursesGlobal = courses;
        $.each(courses, function (index, value) {
            $("#courses").append(`<li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                    <div class="d-flex flex-column text-wrap w-25">
                        <h6 class="mb-1 text-dark font-weight-bold text-sm text-wrap">${value.name}</h6>
                        <span class="text-xs">${value.courseId}</span>
                    </div>
                     <div class="d-flex align-items-center text-sm">
                        ${value.price} VND
                    </div>
                    <div class="d-flex align-items-center text-sm">
                        ${value.isPrivate}
                    </div>
                    <div>
                        <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewCourse" course-id="${value.courseId}">
                            <i class="fas fa-edit" style="color: #38d100; font-size: 20px;"></i>
                            Sửa
                        </button>
                        <a href="javascript:void(0)" data-id="${value.courseId}" class="viewprojectbills">
                            <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                <i class="fas fa-file-pdf text-lg me-1"></i>
                                Xem
                            </button>
                        </a>
                    </div>
                    </li>
            `);
        });


    })


    getAllCatogory().then(categories => {
        categoriesGlobal = categories;
        const categorySelect = document.getElementById('categoryId');
        categories.forEach(category => {
            const option = document.createElement('option');
            option.value = category.categoryId;
            option.textContent = category.name;
            categorySelect.appendChild(option);
        });
    });
}


$('#searchCourse').on('input', function () {
    var searchText = $(this).val().toLowerCase();
    var filteredStudents = coursesGlobal.filter(function (course) {
        return course.name.toLowerCase().indexOf(searchText) !== -1 ;
    });
    // showStudentList(filteredStudents);
    $("#courses").html("");
    $.each(filteredStudents, function (index, value) {
        $("#courses").append(`<li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                    <div class="d-flex flex-column">
                        <h6 class="mb-1 text-dark font-weight-bold text-sm">${value.name}</h6>
                        <span class="text-xs">${value.courseId}</span>
                    </div>
                     <div class="d-flex align-items-center text-sm">
                        ${value.price} VND
                    </div>
                    <div class="d-flex align-items-center text-sm">
                        ${value.name}
                    </div>
                    <a href="javascript:void(0)" course-id="${value.projectId}" class="viewprojectbills">
                        <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                            <i class="fas fa-file-pdf text-lg me-1"></i>
                            Xem
                        </button>
                    </a>
                    </li>
            `);
    });
});

$(document).ready(function () {
    $("#createCourse").click(function () {
        const courseId = $('#courseId').val();
        const categoryId = $('#categoryId').val();
        const name = $('#name').val();
        const image = $('#image').prop('files')[0];
        const description = $('#description').val();
        const isPrivate = $('#isPrivate').prop('checked');
        const price = $('#price').val();


        const formData = new FormData();
        formData.append('courseId', courseId);
        formData.append('categoryId', categoryId);
        formData.append('image', image);
        formData.append('name', name);
        formData.append('description', description);
        formData.append('isPrivate', isPrivate);
        formData.append('price', price);


        if (courseId == 0) {
            $.ajax({
                url: 'https://localhost:5000/api/Courses',
                type: 'POST',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    console.log('Server response:', response);
                    // Làm mới trang sau khi gửi thành công
                    location.reload();
                },
                error: function (error) {
                    console.log('Error:', error);
                }
            });
        } else {
            $.ajax({
                url: `https://localhost:5000/api/Courses/${courseId}`,
                type: 'PUT',
                data: formData,
                contentType: false,
                processData: false,
                success: function (response) {
                    console.log('Server response:', response);
                    // Làm mới trang sau khi gửi thành công
                    location.reload();
                },
                error: function (error) {
                    console.log('Error:', error);
                }
            });
        }

        //$.ajax({
        //    type: "post",
        //    url: "https://localhost:5000/api/Courses",
        //    data: JSON.stringify(formData),
        //    contentType: "application/json",
        //    success: function (result, status, xhr) {
        //        if (confirm('Thêm khóa học thành công!')) {
        //            location.reload();
        //        }
        //    },
        //    error: function (xhr, status, error) {
        //        console.log(xhr)
        //    }
        //});
    });



    $('#clearCourse').click(function () {

        $('#courseId').val('0');
        $('#name').val('');
        $('#description').val('');
        $('#isPrivate').prop('checked', true);
        $('#price').val('0');
        $('#categoryId option:first').prop('selected', true);
    });


    $(document).on('click', '.viewCourse', function () {
        let courseId = $(this).attr('course-id');


        var course = coursesGlobal.find(c => c.courseId == courseId);

        $('#courseId').val(course.courseId);
        $('#name').val(course.name);
        $('#description').val(course.description);
        $('#isPrivate').prop('checked', course.isPrivate);
        $('#price').val(course.price);
        $('#categoryId').val(course.categoryId);
    });


    $(document).on('click', '.viewprojectbills', function () {
        let id = $(this).attr('data-id'); console.log(id);

        var course = coursesGlobal.find(course => course.courseId == id); console.log(coursesGlobal);
        $("#course-detail").html('');
        $("#course-detail").append(`
                            <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    <img width="300px;" src='${course.image}'>
                                </div>
                             </li>

                              <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    ID:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.courseId}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Tên Khóa học:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.name}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Giá bán:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.price}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Chế độ riêng tư:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.isPrivate}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Mô tả:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.description}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Danh mục:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${course.categoryId}
                                </div>
                             </li>
        `);

        
    });
});
