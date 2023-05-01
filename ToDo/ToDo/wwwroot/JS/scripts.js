
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

    $(window).resize(function () {
        console.log($(window).width());
    })

    function matchSortPosition() {
        var createBtnPadTop = $('#createAssignment').outerHeight();

        $('.task-sort').outerHeight(createBtnPadTop);

        console.log(createBtnPadTop);

    }
    matchSortPosition()

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
                if (!url.includes("/Assignment/AssignmentDetails"))
                    handler.getDateTags(document.getElementsByTagName("input")).flatpickr(config);
                
                //console.log(response)
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('error: ', textStatus, errorThrown)
            }
        });
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

    $(document).on('click', "#createSubmit", function () {
        var url = $(this).data('url');
        
        var name = $('#assignmentCreateName').val();
        
        console.log("name: " + name);

        var model = {
            AssignmentName: $("#assignmentCreateName").val(),
            AssignmentDescription: $("#assignmentCreateDescription").val(),
            DueDate: new Date($("#assignmentCreateDate").val()).toISOString(),
        }

        console.log(model);

 
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
                

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('POST error: ', textStatus, errorThrown, jqXHR)
            }

        });

        
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
                

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log('POST error: ', textStatus, errorThrown, jqXHR)
            }
        })



    });


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

    

    /*function setCreateButtonPosition()
    {
        var card = $('#taskHolder').position().left
        var cardList = $('.task-list')
        var btn = $('.sort-container')

        console.log("total position: " + (cardList.offset().left + card))
        console.log("btn pos: " + btn.position().left )

        btn.css('left', cardList.offset().left + card + 'px')
    }

    setCreateButtonPosition()

    $(window).on('resize', function () {
        console.log("adjuested")
        setCreateButtonPosition()
    });*/

    $('.fa-arrow-down').on("click", function () {
        $(this).toggleClass('fa-solid fa-angle-up');
    })

    /*$('.task-holder').on("click", function () {
        const icon = $(this).find('i').attr("class");
        toggleAngle[icon]($(this).find('i'));
        $('.task-desc', this).toggle();
    })*/

    const sortSwitch = {
        "1": (value, list, parent) => {
            list.sort(function (a, b) { return getTargetElement(b, value).localeCompare(getTargetElement(a, value)); })
            const newList = $('<div class="task-col-holder"></div>').append(list);
            console.log(list.find("#assignmentName"));
            parent.empty().append(newList);
            //rearrageHTML(list)
        },
        "2": (value, list, parent) => {
            list.sort(function (a, b) { return getTargetElement(a, value).localeCompare(getTargetElement(b, value)) })
            const newList = $('<div class="task-col-holder"></div>').append(list);
            console.log(list.find("#assignmentName"));
            parent.empty().append(newList);
            //rearrageHTML(list)
        },
        "3": (value, list, parent) => {
            list.sort(function (a, b) { return getTargetElement(b, value).localeCompare(getTargetElement(a, value)) })
            const newList = $('<div class="task-col-holder"></div>').append(list);
            console.log(list.find("#assignmentName"));
            parent.empty().append(newList);
            //rearrageHTML(list)
        },
        "default": console.log("Unknown")
    }

    $(".task-sort").on("change", function () {
        const value = $(this).val();

        const row = $('.task-row')
        var cards = row.find('.task-col-holder');
        console.log(cards)
        sortSwitch[value](value, cards, row);
        

        //sortSwitch[value](value, list) //|| sortSwitch["default"]();

    });

    function getTargetElement(element, value) {
        
        const index = value == 1 ? 0 : 1;
        if (index == 0) {
            //get due date elements by id
            const date = $(element).find("#assignmentDate").html();
            const time = $(element).find("#assignmentTime").html();
            console.log("Date: " + date + " " + time)
            return date + " " + time;
            
        }

        const assignmentName = $(element).find("#assignmentName").html();
        console.log("assignment name:" +  assignmentName)
        return $(element).find("#assignmentName").html();
    }

    function rearrageHTML(elementList) {
        for (var i = 0; i < elementList.length - 1; i++) {
            elementList[i].parentNode.appendChild(elementList[i]);
        }
    }



    //Changes card color if Active
    $(".cd").on("click", function () {
        $('.cd').removeClass('active');
        $(this).addClass('active');
    });
})

