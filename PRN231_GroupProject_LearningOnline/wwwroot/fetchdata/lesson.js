const url = 'https://localhost:5000/api';
const courseId = new URL(window.location.href).searchParams.get('courseId') || 1;
const lessonNum = new URL(window.location.href).searchParams.get('lessonNum') || 1;
let options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };

// Init When Load pageconst initPage = () => {
const initPage = async () => {
    document.getElementById('projectsActive').className = 'active';
    //document.getElementById("ProjectIdMomo").value = projectId;
    //document.getElementById("ProjectIdVnPay").value = projectId;
    //console.log(document.getElementById("ProjectIdVnPay").value)
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

async function getCourseById(id) {
    const callApi = async (url) => {
        return (await fetch(url)).json();
    }
    return await callApi(`${url}/CourseEnrolls/getcourseenroll/${id}`);
}
// -------------------------------------------------------------------------------
let courseenrollGlobal = {};
async function pushDataOnLoad() {
    const container = document.getElementById('projectContainer');
    const topDonateContainer = document.getElementById('topDonate');
    const listFeatureContainer = document.getElementById('listFeature');

    getCourseById(courseId).then(async (courseenroll) => {
        courseenrollGlobal = courseenroll;
        let course = courseenroll.course;
        //const [amountData] = await Promise.all([getTotalAmountProject(projectId)]);
        let html = `<div class="course_container">
                    <div class="course_title">${course.lessons[lessonNum - 1].name}</div>
                    <div class="w-100" id="player"></div>
                    <div class="course_tabs_container">
                        <div class="tabs d-flex flex-row align-items-center justify-content-start">
								<div class="tab active">Description</div>
								<div class="tab">Quiz</div>
							</div>
                        <div class="tab_panels">
                            <div class="tab_panel active">
                                ${course.lessons[lessonNum - 1].description}
                            </div>
                            <div class="tab_panel tab_panel_2">
                                <ul>
                                    {{quiz}}
                                </ul>
                            </div>
                        </div>

                    </div>
                `;
        let lessons = course.lessons;
        let lesson = course.lessons.find(obj => obj.lessonNum == lessonNum);
        let featureHtml = ``;
        $.each(lessons, function (index, value) {
            //$('#myList').append('<li>' + value + '</li>');
            if (value.lessonNum <= courseenroll.lessonCurrent) {
                featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                                <div class="feature_title">
                                <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                <span><a class="text" href='https://localhost:5000/courses/lesson?courseId=${courseId}&lessonNum=${value.lessonNum}'>${value.name}</a></span></div>
                                <div class="feature_text ml-auto">10:34</div>
                            </div>`;
            } else {
                featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                                <div class="feature_title">
                                <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                <span class="text-black-50">${value.name}</span></div>
                                <div class="feature_text ml-auto">10:34</div>
                            </div>`;
            }
            
        });

        let quiz = '';
        $.each(lesson.quizes, function (index, value) {
            quiz += `<li class="row mb-3">
                        <div class="col-md-12">${value.question}</div>
                        <div class="col-md-6"><input type="radio" name="question-${courseId}-${lesson.lessonId}-${value.questionNo}" value="A" />A. ${value.answerA}</div>
                        <div class="col-md-6"><input type="radio" name="question-${courseId}-${lesson.lessonId}-${value.questionNo}" value="B" />B. ${value.answerB}</div>
                        <div class="col-md-6"><input type="radio" name="question-${courseId}-${lesson.lessonId}-${value.questionNo}" value="C" />C. ${value.answerC}</div>
                        <div class="col-md-6"><input type="radio" name="question-${courseId}-${lesson.lessonId}-${value.questionNo}" value="D" />D. ${value.answerD}</div>
                    </li>`;
        });
        quiz += '<div class="d-flex justify-content-center"><button class="btn btn-info " id="btn-submit-quiz">Chấm điểm</button></div>';
        html = html.replace("{{quiz}}", quiz);
        container.innerHTML += html;
        listFeatureContainer.innerHTML += featureHtml;
        onYouTubeIframeAPIReady(course.lessons[lessonNum - 1].videoUrl);
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
        $("#btn-submit-quiz").click(function (e) {
            let requestData = [];
            // Lấy tất cả các radio được chọn
            $('ul input[type="radio"]:checked').each(function () {
                requestData.push($(this).attr('name') +'-'+ $(this).val());
            });
            console.log(JSON.stringify(requestData));
            $.ajax({
                url: url + '/courses/grade',
                type: "post",
                data: JSON.stringify(requestData),
                contentType: "application/json",
                success: function (result, status, xhr) {
                    if (status == 'success') {
                        alert('Kết quả của bạn là :' + result.result);
                    }
                    //var str = "<tr><td>" + result["id"] + "</td><td>" + result["name"] + "</td><td>" + result["startLocation"] + "</td><td>" + result["endLocation"] + "</td></tr>";
                    //$("table tbody").append(str);
                    //$("#resultDiv").show();
                },
                error: function (xhr, status, error) {
                    console.log(xhr)
                }
            });
        });
    };

    //getTopDonateById(projectId).then(donateData => {
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


    var player;

    // This function creates an <iframe> (and YouTube player)
    // after the API code downloads.
    function onYouTubeIframeAPIReady(videoId) {
        player = new YT.Player('player', {
            height: '390',
            width: '640',
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

                    $.ajax({
                        url: url + `/CourseEnrolls/${courseenrollGlobal.courseEnrollId}`,
                        type: "put",
                        data: JSON.stringify(courseenrollGlobal),
                        contentType: "application/json",
                        success: function (result, status, xhr) {
                            console.log(result);
                            if (status == 'success') {
                                let featureHtml = ``;
                                $.each(result.course.lessons, function (index, value) {
                                    //$('#myList').append('<li>' + value + '</li>');
                                    if (value.lessonNum <= result.lessonCurrent) {
                                        featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                                                        <div class="feature_title">
                                                        <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                                        <span><a class="text" href='https://localhost:5000/courses/lesson?courseId=${courseId}&lessonNum=${value.lessonNum}'>${value.name}</a></span></div>
                                                        <div class="feature_text ml-auto">10:34</div>
                                                    </div>`;
                                    } else {
                                        featureHtml += `<div class="feature d-flex flex-row align-items-center justify-content-start">
                                                        <div class="feature_title">
                                                        <i class="fa fa-file-text-o" aria-hidden="true"></i>
                                                        <span class="text-black-50">${value.name}</span></div>
                                                        <div class="feature_text ml-auto">10:34</div>
                                                    </div>`;
                                    }

                                });
                                document.getElementById('listFeature').innerHTML = featureHtml;
                            }
                        },
                        error: function (xhr, status, error) {
                            console.log(xhr)
                        }
                    });
                }
            }, 1000);
        }
    }

}



