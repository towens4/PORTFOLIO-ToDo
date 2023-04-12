
//var sort_by_date = function (a, b) { return getTargetElement(a, value).localeCompare(getTargetElement(b, value)); }

class DomHandler{
    getDateTags(inputTaskList) {
        for (let i = 0; i < inputTaskList.length; i++)
        {
            var tag = inputTaskList[i];
            if (tag.id == "assignmentCreateDate" || tag.id == "assignmentEditDueDate")
                return tag
        }
    }
}

function convertDateTime(dateTimeStr) {
    
    const [dateStr, timeStr] = dateTimeStr.split(" ");

    
    const date = new Date(dateStr);

    
    const [time, ampm] = timeStr.split(" ");
    const [hours, minutes] = time.split(":");

    
    let newHours = parseInt(hours);
    if (ampm === "PM" && newHours < 12) {
        newHours += 12;
    } else if (ampm === "AM" && newHours === 12) {
        newHours = 0;
    }

    const newDate = new Date(date.getTime());
    newDate.setHours(newHours);
    newDate.setMinutes(parseInt(minutes));

    
    const newDateTimeStr = newDate.toISOString().substring(0, 10) + " " + newDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

    return newDateTimeStr;
}


const sortSwitch = {
    "1": (value, list) => {
        list.sort(function (a, b) { return getTargetElement(b, value).localeCompare(getTargetElement(a, value)); })
        rearrageHTML(list)
    },
    "2": (value, list) => {
        list.sort(function (a, b) { return getTargetElement(a, value).localeCompare(getTargetElement(b, value)) })
        rearrageHTML(list)
    },
    "3": (value, list) => {
        list.sort(function (a, b) { return getTargetElement(b, value).localeCompare(getTargetElement(a, value)) })
        rearrageHTML(list)
    },
    "default": console.log("Unknown")
}

const toggleAngle = {
    "fa-solid fa-angle-up": (element) => {
        element.removeClass("fa-angle-up");
        element.addClass("fa-solid fa-angle-down");
    },
    "fa-solid fa-angle-down": (element) => {
        element.removeClass("fa-angle-down");
        element.addClass("fa-solid fa-angle-up");
    }
}



$(document).ready(function () {

    config = {
        enableTime: true,
        dateFormat: "Y/m/d h:i K",
        time_24Hour: false,
        amPm: true
    }

    var baseUrl = "https://localhost:7063/";
    
    function GetPageByAJAX(url) {
        $.ajax({
            url: url,
            method: 'GET',
            success: function (response) {
                $('#taskIndexView').html(response);
                const handler = new DomHandler();
                handler.getDateTags(document.getElementsByTagName("input")).flatpickr(config);
                //document.getElementById("assignmentCreateDate").flatpickr(config);
                console.log(response)
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('error: ', textStatus, errorThrown)
            }
        });
    }

    function AJAXPOSTXML(formData, url) {
        console.log("About to initiate Post request");
        var xhr = new XMLHttpRequest();
        xhr.open('POST', url);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
        xhr.onload = function () {
            if (xhr.status === 200) {
                console.log(xhr.responseText);
                $("#taskIndexView").html(xhr.responseText);
            }
            else {
                console.log('POST error: ' + xhr.status);
            }
        };
        xhr.onerror = function () {
            console.log('POST error: ', textStatus, errorThrown);
        };
        xhr.send(JSON.stringify(formData));
    }

    function PostAJAX(formData, url) {
        console.log("About to initate Post request")
        $.ajax({
            type: 'POST',
            url: url,
            contentType: 'application/json',
            headers: {
                "X-Requested-With": "XMLHttpRequest"
            },
            data: JSON.stringify(formData),
            success: function (result) {
                //console.log(result);
                $("#taskIndexView").html(result);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('POST error: ', textStatus, errorThrown, jqXHR)
            }

        });
    }

    $(document).on("click", function () {
        var clickedElement = $(event.target);
        console.log(clickedElement);
    });

    function GenerateAjax(url) {

        console.log(url)
        /*$('#taskView').load(url);*/
        GetPageByAJAX(url)
    }

    $(document).on("click", ".task-anchor", function () {
        console.log("The anchor is fired");
        console.log("");
        /*if (docUrl.includes("AssignmentDetails"))
        {
            $('#content').toggle();
            
        }*/
        GenerateAjax($(this).data('url'));

    })

    $(document).on("click", "#editAssignment", function () {
        GenerateAjax($(this).data('url'))
    })

    $(document).on("click", "#createAssignment", function () {
        GenerateAjax($(this).data('url'))
    })

    /*flatpickr("#assignmentCreateTime", {
        enableTime: true,
        noCalendar: true,
        dateFormat: "h:i k",
        time_24Hour: false,
        amPm: true
    })*/

    
   // $(".time-select").flatpickr(config);
    

    $(document).on('click', "#createSubmit", function () {
        var url = $(this).data('url');
        //var model = $("#createFormSubmit");

        //console.log("Form Data: " + model);
        //document.getElementById("assignmentName").innerHTML();
        var name = $('#assignmentCreateName').val();

        var timeType = $("#timeType").val();

        var time = $("#assignmentCreateTime").val() + " " + timeType
        
        console.log("name: " + name);

        var model = {
            AssignmentName: $("#assignmentCreateName").val(),
            AssignmentDescription: $("#assignmentCreateDescription").val(),
            DueDate: new Date($("#assignmentCreateDate").val()).toISOString(),
        }

        console.log(model);

        var jsonModel = JSON.stringify({
            model: {
                'AssignmentName': $("#assignmentName").val(),
                'AssignmentDescription': $("#assignmentDescription").val(),
                'DueDate': $("#assignmentDate").val()
            }
        })

        $.ajax({
            type: 'POST',
            url: url,
            contentType: 'application/json',
            headers: {
                "X-Requested-With": "XMLHttpRequest"
            },
            data: JSON.stringify(model),
            success: function (result, status, jqXHR) {
                if (jqXHR.getResponseHeader("is-valid") == "false") {
                    $("#taskIndexView").html(result);
                }
                else {
                    $("#taskIndexView").empty();
                    window.location.replace(baseUrl);
                }
                //Window.Location.reload(true);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('POST error: ', textStatus, errorThrown, jqXHR)
            }

        });

        //PostAJAX(model, url);
        //AJAXPOSTXML(formData, url)
    });

    $(document).on('click', "#editSubmit", function () {
        var url = $(this).data('url');
        var model = {
            AssignmentID: $('#assignmentEditID').val(),
            AssignmentName: $('#assignmentEditName').val(),
            AssignmentDescription: $('#assignmentEditDescription').val(),
            DueDate: new Date($('#assignmentEditDueDate').val()).toISOString()
        }

        $.ajax({
            type: 'POST',
            url: url,
            contentType: 'application/json',
            headers: {
                "X-Requested-With": "XMLHttpRequest"
            },
            data: JSON.stringify(model),
            success: function (result, status, jqXHR) {
                if (jqXHR.getResponseHeader("is-valid") == "false") {
                    $("#taskIndexView").html(result);
                }
                else {
                    $("#taskIndexView").empty();
                    window.location.replace(baseUrl);
                }
                //Window.Location.reload(true);

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('POST error: ', textStatus, errorThrown, jqXHR)
            }
        })


        $("#formSubmit").submit(function (event) {
            event.preventDefault();

            var url = $(this).data('url');
            var formData = $(this).serialize();

            var model = {
                AssignmentName: $('assignmentName').val(),
                AssignmentDescription: $('assignmentDescription'),
                DueDate: $('assignmentDate').val()
            }

            console.log("Form Data: " + formData);

            PostAJAX(formData, url)
        })

        /*$('.task-holder').on("click", function () {
            console.log("The anchor is fired");
            var url = $(this).attr('href');
            console.log(url)
            $('#taskView').load(url);
        })*/

        $('.fa-arrow-down').on("click", function () {
            $(this).toggleClass('fa-solid fa-angle-up');
        })

        $('.task-holder').on("click", function () {
            const icon = $(this).find('i').attr("class");
            toggleAngle[icon]($(this).find('i'));
            $('.task-desc', this).toggle();
        })

        $(".task-sort").on("change", function () {
            const value = $(this).val();
            var list = $('.task-holder').get();

            sortSwitch[value](value, list) //|| sortSwitch["default"]();

        });

        $(".cd").on("click", function () {
            $('.cd.active').removeClass('active');
            $(this).addClass('active');
        });

    });
})

function getTargetElement(element, value)
{
    const index = value == 0 ? 1 : 0;
    if (index == 0)
    {
        //get due date elements by id
        const date = $("#assignmentDate");
        const time = $("#assignmentTime");
        return date + " " + time;
        console.log(element.firstElementChild.children[1]);
    }
    console.log(index)
    return $("#assignmentDescription");
}

function rearrageHTML(elementList)
{
    for (var i = 0; i < elementList.length - 1; i++)
    {
        elementList[i].parentNode.appendChild(elementList[i]);
    }
}