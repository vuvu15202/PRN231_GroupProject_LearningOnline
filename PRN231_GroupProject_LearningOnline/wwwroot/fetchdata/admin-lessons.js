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

        const courseSelect = document.getElementById('courseId');
        const courseSelectCreate = document.getElementById('courseIdCreate');

        courses.forEach(category => {
            const option = document.createElement('option');
            option.value = category.courseId;
            option.textContent = category.name;
            courseSelectCreate.appendChild(option);
        });
        courses.forEach(category => {
            const option = document.createElement('option');
            option.value = category.courseId;
            option.textContent = category.name;
            courseSelect.appendChild(option);
        });
        //$.each(courses, function (index, value) {
        //    $("#courses").append(`<li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
        //            <div class="d-flex flex-column">
        //                <h6 class="mb-1 text-dark font-weight-bold text-sm">${value.name}</h6>
        //                <span class="text-xs">${value.courseId}</span>
        //            </div>
        //             <div class="d-flex align-items-center text-sm">
        //                ${value.price} VND
        //            </div>
        //            <div class="d-flex align-items-center text-sm">
        //                ${value.name}
        //            </div>
        //            <a href="javascript:void(0)" data-id="${value.projectId}" class="viewprojectbills">
        //                <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
        //                    <i class="fas fa-file-pdf text-lg me-1"></i>
        //                    Xem
        //                </button>
        //            </a>
        //            </li>
        //    `);
        //});


    })


    
}


$('#courseId').on('change', function () {
    const courseId = $(this).val();
    const courseName = $(this).find("option:selected").text();

    var filteredStudents = coursesGlobal.find(c => c.courseId == courseId);
    // showStudentList(filteredStudents);
    $("#lessons").html("");
    $.each(filteredStudents.lessons, function (index, value) {
        $("#lessons").append(`<li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                    <div class="d-flex align-items-center text-sm">
                        <h6 class="mb-1 text-dark font-weight-bold text-sm">${value.lessonNum}:</h6>
                    </div>
                    <div class="d-flex align-items-center text-sm">
                        <span class="text-xs">${value.name}</span>
                    </div>
                    <div class="d-flex align-items-center text-sm">
                        url: ${value.videoUrl}
                    </div>
                    <div>
                        <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4 viewLesson" course-id="${courseId}" lesson-num="${value.lessonNum}">
                                    <i class="fas fa-edit" style="color: #38d100; font-size: 20px;"></i>
                                    Sửa
                        </button>
                        <a href="javascript:void(0)" course-id="${courseId}" lesson-num="${value.lessonNum}" class="viewprojectbills">
                            <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                                <i class="fas fa-file-pdf text-lg me-1"></i>
                                Xem
                            </button>
                        </a>
                    </div>
                    
                    </li>
            `);
    });
});


//$('#searchCourse').on('input', function () {
//    var searchText = $(this).val().toLowerCase();
//    var filteredStudents = coursesGlobal.filter(function (course) {
//        return course.name.toLowerCase().indexOf(searchText) !== -1 ;
//    });
//    // showStudentList(filteredStudents);
//    $("#courses").html("");
//    $.each(filteredStudents, function (index, value) {
//        $("#courses").append(`<li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
//                    <div class="d-flex flex-column">
//                        <h6 class="mb-1 text-dark font-weight-bold text-sm">${value.name}</h6>
//                        <span class="text-xs">${value.courseId}</span>
//                    </div>
//                     <div class="d-flex align-items-center text-sm">
//                        ${value.price} VND
//                    </div>
//                    <div class="d-flex align-items-center text-sm">
//                        ${value.name}
//                    </div>
//                    <a href="javascript:void(0)" course-id="${value.projectId}" class="viewprojectbills">
//                        <button class="btn btn-link text-dark text-sm mb-0 px-0 ms-4" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
//                            <i class="fas fa-file-pdf text-lg me-1"></i>
//                            Xem
//                        </button>
//                    </a>
//                    </li>
//            `);
//    });
//});

$(document).ready(function () {
    
    $('#createLesson').click(function () {
        // Hiển thị hộp thoại xác nhận
        if (confirm('Are you sure you want to submit this form?')) {
            // Lấy giá trị của tất cả các input
            const lessonId = $('#lessonId').val();
            const lessonNum = $('#lessonNum').val();
            const courseId = $('#courseIdCreate').val();
            const name = $('#name').val();
            const description = $('#description').val();
            const videoUrl = $('#videoUrl').val();
            const quiz = $('#quiz').prop('files')[0];
            const previousLessioNum = $('#previousLessioNum').val();

            // Tạo một đối tượng FormData để chứa các giá trị
            const formData = new FormData();
            formData.append('lessonId', lessonId);
            formData.append('lessonNum', lessonNum);
            formData.append('courseId', courseId);
            formData.append('name', name);
            formData.append('description', description);
            formData.append('videoUrl', videoUrl);
            formData.append('quiz', quiz);
            formData.append('previousLessioNum', previousLessioNum);

            // In ra console để kiểm tra (chỉ có thể in ra các giá trị text)
            console.log('Lesson ID:', lessonId);
            console.log('Lesson Number:', lessonNum);
            console.log('Course ID:', courseId);
            console.log('Name:', name);
            console.log('Description:', description);
            console.log('Video URL:', videoUrl);
            console.log('Quiz File:', quiz ? quiz.name : 'No file selected');
            console.log('Previous Lesson Number:', previousLessioNum);

            if (lessonId == 0) {
                $.ajax({
                    url: 'https://localhost:5000/api/Lessons',
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
                    url: `https://localhost:5000/api/Lessons/${lessonId}`,
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
             

        }
    });


    $('#clearLesson').click(function () {
        
        $('#lessonId').val('0');
        $('#lessonNum').val('1');
        $('#courseIdCreate option:first').prop('selected', true);
        $('#name').val('');
        $('#description').val('');
        $('#videoUrl').val('');
        $('#previousLessioNum').val('0');
        $('#courseId').val('');
    });


    $(document).on('click', '.viewLesson', function () {
        let courseId = $(this).attr('course-id');
        let lessonNum = $(this).attr('lesson-num');
        console.log(lesson);


        var course = coursesGlobal.find(c => c.courseId == courseId);
        var lesson = course.lessons.find(l => l.lessonNum == lessonNum);

        $('#lessonId').val(lesson.lessonId);
        $('#lessonNum').val(lesson.lessonNum);
        $('#courseIdCreate').val(lesson.courseId);
        $('#name').val(lesson.name);
        $('#description').val(lesson.description);
        $('#videoUrl').val(lesson.videoUrl);
        $('#previousLessioNum').val(lesson.previousLessioNum);
        $('#courseId').val(lesson.courseId);
    });


    $(document).on('click', '.viewprojectbills', function () {
        let courseId = $(this).attr('course-id');
        let lessonNum = $(this).attr('lesson-num');


        var course = coursesGlobal.find(c => c.courseId == courseId); 
        var lesson = course.lessons.find(l => l.lessonNum == lessonNum);
        $("#course-detail").html('')
        $("#course-detail").append(`
                            <li class="list-group-item border-0 ">
                                <div>
                                    <div class="w-100" id="player"></div>
                                </div>
                             </li>

                              <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    LessonNum:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${lesson.lessonNum}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Tên Bài Giảng:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${lesson.name}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Mô tả:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${lesson.description}
                                </div>
                             </li>
                             <li class="list-group-item border-0 d-flex justify-content-between ps-0 mb-2 border-radius-lg">
                                <div>
                                    Video Url:
                                </div>
                                <div class="d-flex align-items-center text-success text-gradient text-sm font-weight-bold w-75">
                                    ${lesson.videoUrl}
                                </div>
                             </li>
                             
        `);
        $("#course-detail").append(`
            <hr>
            <h2>Đề kiểm tra</h2>
            <div class="row"> <div class="col">`);

        lesson.quizes.forEach(quiz => {
            if (quiz.correctAnswer == 'A') {
                const row = `
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Câu hỏi ${quiz.questionNo}: ${quiz.question}?</h5>
                        <div class="row">
                            <div class="col-6">
                                <p class="card-text answer text-success">A. ${quiz.answerA}</p>
                            </div>
                            <div class="col-6">
                                <p class="card-text answer">B. ${quiz.answerB}</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <p class="card-text answer">C. ${quiz.answerC}</p>
                            </div>
                            <div class="col-6">
                                <p class="card-text answer">D. ${quiz.answerD}</p>
                            </div>
                        </div>
                    </div>
                </div>`;
                $("#course-detail").append(row);
            } else if (quiz.correctAnswer == 'B') {
                const row = `
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Câu hỏi ${quiz.questionNo}: ${quiz.question}?</h5>
                        <div class="row">
                            <div class="col-6">
                                <p class="card-text answer">A. ${quiz.answerA}</p>
                            </div>
                            <div class="col-6">
                                <p class="card-text answer text-success">B. ${quiz.answerB}</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <p class="card-text answer">C. ${quiz.answerC}</p>
                            </div>
                            <div class="col-6">
                                <p class="card-text answer">D. ${quiz.answerD}</p>
                            </div>
                        </div>
                    </div>
                </div>`;
                $("#course-detail").append(row);
            } else if (quiz.correctAnswer == 'C') {
                const row = `
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Câu hỏi ${quiz.questionNo}: ${quiz.question}?</h5>
                        <div class="row">
                            <div class="col-6">
                                <p class="card-text">A. ${quiz.answerA}</p>
                            </div>
                            <div class="col-6">
                                <p class="card-text">B. ${quiz.answerB}</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <p class="card-text answer text-success">C. ${quiz.answerC}</p>
                            </div>
                            <div class="col-6">
                                <p class="card-text answer">D. ${quiz.answerD}</p>
                            </div>
                        </div>
                    </div>
                </div>`;
                $("#course-detail").append(row);
            } else {
                const row = `
                <div class="card mb-3">
                    <div class="card-body">
                        <h5 class="card-title">Câu hỏi ${quiz.questionNo}: ${quiz.question}?</h5>
                        <div class="row">
                            <div class="col-6">
                                <p class="card-text answer">A. ${quiz.answerA}</p>
                            </div>
                            <div class="col-6">
                                <p class="card-text answer">B. ${quiz.answerB}</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <p class="card-text answer">C. ${quiz.answerC}</p>
                            </div>
                            <div class="col-6">
                                <p class="card-text answer text-success">D. ${quiz.answerD}</p>
                            </div>
                        </div>
                    </div>
                </div>`;
                $("#course-detail").append(row);
            }
            
        });

        $("#course-detail").append(`
            </div>
            </div>`)

        onYouTubeIframeAPIReady(lesson.videoUrl);
        
    });




    //youtube
    var player;

    // This function creates an <iframe> (and YouTube player)
    // after the API code downloads.
    function onYouTubeIframeAPIReady(videoId) {
        player = new YT.Player('player', {
            height: '600',
            width: '1200',
            videoId: videoId, // Thay thế VIDEO_ID bằng ID của video YouTube
            events: {
                'onReady': onPlayerReady,
                'onStateChange': onPlayerStateChange
            }
        });
    }

    // The API will call this function when the video player is ready.
    function onPlayerReady(event) {
        // Bắt đầu phát video (tùy chọn)
        // event.target.playVideo();
    }

    function onPlayerStateChange(event) {
        if (event.data == YT.PlayerState.PLAYING) {
            setInterval(function () {
                var currentTime = player.getCurrentTime();
                if (currentTime > 10 && lessonNum == courseenrollGlobal.lessonCurrent) {
                    delete courseenrollGlobal['course'];
                    delete courseenrollGlobal['user'];
                    courseenrollGlobal.lessonCurrent = courseenrollGlobal.lessonCurrent + 1;

                    //$.ajax({
                    //    url: url + `/CourseEnrolls/${courseenrollGlobal.courseEnrollId}`,
                    //    type: "put",
                    //    data: JSON.stringify(courseenrollGlobal),
                    //    contentType: "application/json",
                    //    success: function (result, status, xhr) {
                    //        console.log(result);
                    //        if (status == 'success') {
                    //            let featureHtml = ``;
                    //            $.each(result.course.lessons, function (index, value) {
                    //                //$('#myList').append('<li>' + value + '</li>');
                    //                if (value.lessonNum <= result.lessonCurrent) {
                    //                    featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                    //                                    <div class="feature_title">
                    //                                    <i class="fa fa-file-text-o" aria-hidden="true"></i>
                    //                                    <span><a class="text" href='https://localhost:5000/courses/lesson?courseId=${courseId}&lessonNum=${value.lessonNum}'>${value.name}</a></span></div>
                    //                                    <div class="feature_text ml-auto">10:34</div>
                    //                                </div>`;
                    //                } else {
                    //                    featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                    //                                    <div class="feature_title">
                    //                                    <i class="fa fa-file-text-o" aria-hidden="true"></i>
                    //                                    <span class="text-black-50">${value.name}</span></div>
                    //                                    <div class="feature_text ml-auto">10:34</div>
                    //                                </div>`;
                    //                }

                    //            });
                    //            document.getElementById('listFeature').innerHTML = featureHtml;
                    //        }
                    //    },
                    //    error: function (xhr, status, error) {
                    //        console.log(xhr)
                    //    }
                    //});
                }
            }, 1000);
        }
    }
});
