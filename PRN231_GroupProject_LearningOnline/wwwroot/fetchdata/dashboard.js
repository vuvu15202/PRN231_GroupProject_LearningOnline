

$(document).ready(function () {
    var projectbills;
    var handlingbills;
    var token = getCookie("token");

    getData();
    function getData() {
        //ShowProjectBills();
        //setTimeout(showHandlingbills, 500);
        setTimeout(ShowReport, 500);
    }




    function ShowReport() {
        $.ajax({
            url: "https://localhost:5000/api/Dashboard/report",
            type: "get",
            headers: {
                "Authorization": "Bearer " + token,
            },
            contentType: "application/json",
            success: function (result, status, xhr) {
                $("#report1").html('').append(result.todayMoney);
                $("#report2").html('').append(result.todayUser);
                $("#report3").html('').append(result.newClient);
                $("#report4").html('').append(result.newEnroll);
                renderStatistic(result.statistic);
                renderhandlingstudentfees(result.handlingstudentfees);
                $.each(result.courses, function (index, value) {
                    $("#courseId").append(`<option value="${value.id}">${value.name}</option>`);
                });
            },
            error: function (xhr, status, error) {
                console.log(xhr)
            }
        });

    }

    function renderhandlingstudentfees(data) {
        $.each(data, function (index, value) {
            $("#handlingstudentfees").append(`<tr>
                    <td class="w-30">
                        <div class="d-flex px-2 py-1 align-items-center">
                            <div>
                                <i class="fas fa-arrow-up"></i>
                            </div>
                            <div class="ms-4">
                                <p class="text-xs font-weight-bold mb-0">Thông tin:</p>
                                <h6 class="text-sm mb-0">${value.orderInfo}</h6>
                            </div>
                        </div>
                    </td>
                    <td>
                        <div class="text-center">
                            <p class="text-xs font-weight-bold mb-0">Số tiền:</p>
                            <h6 class="text-sm mb-0">${value.amount}</h6>
                        </div>
                    </td>
                    <td>
                        <div class="text-center">
                            <p class="text-xs font-weight-bold mb-0">Phương thức TT:</p>
                            <h6 class="text-sm mb-0">${value.paymentMethod}</h6>
                        </div>
                    </td>
                    <td class="align-middle text-sm">
                        <div class="col text-center">
                            <p class="text-xs font-weight-bold mb-0">Thời gian:</p>
                            <h6 class="text-sm mb-0">${value.dateOfPaid}</h6>
                        </div>
                    </td>
                </tr>`);
        });
    }


    $(document).on('click', '#btnFilderStatistic', function () {
        var courseId = $("#courseId").val();
        var year = $("#year").val();

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

    function getCookie(name) {
        // Split cookies by semicolon
        var cookies = document.cookie.split(';');

        // Loop through each cookie
        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i];
            // Trim leading and trailing spaces
            cookie = cookie.trim();
            // Check if this cookie is the one we're looking for
            if (cookie.startsWith(name + '=')) {
                // Return the cookie value
                return cookie.substring(name.length + 1);
            }
        }
        // Return null if cookie not found
        return null;
    }

    function renderStatistic(data) {
        $(".chart").html('<canvas id="chart-line" class="chart-canvas" height="300"></canvas>');
        const chartContainer = document.getElementById("chart-line");

        var ctx1 = chartContainer.getContext("2d");

        var gradientStroke1 = ctx1.createLinearGradient(0, 230, 0, 50);

        gradientStroke1.addColorStop(1, 'rgba(94, 114, 228, 0.2)');
        gradientStroke1.addColorStop(0.2, 'rgba(94, 114, 228, 0.0)');
        gradientStroke1.addColorStop(0, 'rgba(94, 114, 228, 0)');
        new Chart(ctx1, {
            type: "line",
            data: {
                labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                datasets: [{
                    label: "Student Fee",
                    tension: 0.4,
                    borderWidth: 0,
                    pointRadius: 0,
                    borderColor: "#5e72e4",
                    backgroundColor: gradientStroke1,
                    borderWidth: 3,
                    fill: true,
                    data: data,
                    maxBarThickness: 6

                }],
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        display: false,
                    }
                },
                interaction: {
                    intersect: false,
                    mode: 'index',
                },
                scales: {
                    y: {
                        grid: {
                            drawBorder: false,
                            display: true,
                            drawOnChartArea: true,
                            drawTicks: false,
                            borderDash: [5, 5]
                        },
                        ticks: {
                            display: true,
                            padding: 10,
                            color: '#fbfbfb',
                            font: {
                                size: 11,
                                family: "Open Sans",
                                style: 'normal',
                                lineHeight: 2
                            },
                        }
                    },
                    x: {
                        grid: {
                            drawBorder: false,
                            display: false,
                            drawOnChartArea: false,
                            drawTicks: false,
                            borderDash: [5, 5]
                        },
                        ticks: {
                            display: true,
                            color: '#ccc',
                            padding: 20,
                            font: {
                                size: 11,
                                family: "Open Sans",
                                style: 'normal',
                                lineHeight: 2
                            },
                        }
                    },
                },
            },
        });
    }

});
