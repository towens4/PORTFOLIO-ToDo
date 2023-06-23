
//var sort_by_date = function (a, b) { return getTargetElement(a, value).localeCompare(getTargetElement(b, value)); }

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

    var taskCompletedBackup;
    var domHandler =
    {
        getDateTags: function (inputTaskList)
        {
            for (let i = 0; i < inputTaskList.length; i++) {
                var tag = inputTaskList[i];
                if (tag.id == "assignmentCreateDate" || tag.id == "assignmentEditDueDate")
                    return tag
            }
        }
    }

    // Initialize event logger
    eventLogger.initialize();

    

    dueDateHandler()

    $(window).resize(function () {
        console.log($(window).width());
    })

    function dueDateHandler() {
        
        if ($('.task-anchor').length) {

            $('.task-anchor').each(function (index, card) {
                // Get the due date from the card
                var dateParts = $(card).find('#assignmentDate').html().split('/');
                var dueDate = new Date(dateParts[2], dateParts[1] - 1, dateParts[0]);
                var isCompleted = $(card).find("#assignmentCompletion").html()
                var taskStatus = $(card).find(".task-status");
                

                // Check if the task is overdue, due today, or not due yet
                var today = new Date();
                if (dueDate < today)
                {
                    console.log("Task is overdue");
                    $(card).find(".cd").toggleClass("overdue");
                    $(taskStatus).toggleClass("overdue");
                    $(taskStatus).html("overdue");
                    
                }
                else if (dueDate.getTime() === today.getTime())
                {
                    console.log("Task is due today");
                }
                else
                {
                    if ($(card).find(".cd").hasClass("overdue")) {
                        $(card).find(".cd").toggleClass("overdue");
                    }
                    if ($(taskStatus).hasClass("overdue")) {
                        $(taskStatus).toggleClass("overdue")
                        $(taskStatus).html("");
                    }
                    else{
                        $(taskStatus).toggle();
                        $(taskStatus).html("");
                    }
                        
                    console.log("Task is not due yet");
                }

                if (isCompleted == "True")
                {
                    $(card).find(".cd").toggleClass("completed");
                    $(taskStatus).toggle();
                    $(taskStatus).toggleClass("completed-task-status");
                    $(taskStatus).html("completed");
                }
            });
        }
    }

    

        $(document).on("click", ".list-switch", function () {
            var listType = $(this).data("type");
            const selectedElement = $(this)
            const buttons = $('.list-switch');
            console.log(buttons)
            
            $(buttons).removeClass("list-switch-active");
            $(selectedElement).addClass("list-switch-active");

            const isCompleted = listType == "completed" ? true : false;

            AJAXRequest('GET', '/Assignment/UpdateListComponent', null, { "completed-flag": isCompleted }, function (response, status, jqXHR) {
                const taskContainer = $('#taskListContainer')
                $(taskContainer).html(response);
                const listOptions = taskContainer.find('.list-switch');
                console.log(listOptions)
                listOptions.removeClass('list-switch-active');
                //isCompleted === true
                $(listOptions).each(function () {
                    const status = $(this).data('type');
                    if (isCompleted === true)
                        $('[data-type="completed"]').addClass('list-switch-active')
                    else
                        $('[data-type="incomplete"]').addClass('list-switch-active')
                })

                dueDateHandler()
                
                const tempButtons = buttons;
                const tempCurrent = selectedElement;

                
                $(buttons).removeClass("list-switch-active")
                
                $(selectedElement).addClass("list-switch-active");
                    
            }, function (jqXHR, textStatus, errorThrown) {
                console.log('error: ', textStatus, errorThrown)
            })
            

         
        })


    config = {
        enableTime: true,
        dateFormat: "Y/m/d h:i K",
        time_24Hour: false,
        amPm: true
    }

    var baseUrl = "https://localhost:7063/";

    

    function GetPageByAJAX(url) {

        AJAXRequest('GET', url, null, null, function (response, status, jqXHR) {
            $('#taskIndexView').html(response);
            if (!url.includes("/Assignment/AssignmentDetails"))
            {

                domHandler.getDateTags(document.getElementsByTagName("input")).flatpickr(config);
            }

        }, function (jqXHR, textStatus, errorThrown) {
            console.log('error: ', textStatus, errorThrown)
        });
        
    }

    $(document).on("click", function () {
        var clickedElement = $(event.target);
        console.log(clickedElement);
    });

    function GenerateAjax(url) {

        console.log(url)
        
        GetPageByAJAX(url)
    }

    $(document).on("click", ".task-anchor", function () {
        console.log("The anchor is fired");
        
       
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
        
        console.log($("#assignmentCreateComplete").is(":checked"));

        var model = {
            AssignmentName: $("#assignmentCreateName").val(),
            AssignmentDescription: $("#assignmentCreateDescription").val(),
            DueDate: new Date($("#assignmentCreateDate").val()).toISOString(),
            Completed: $("#assignmentCreateComplete").is(":checked")
        }

        console.log(model);

        AJAXRequest('POST', url, model, { "X-Requested-With": "XMLHttpRequest" }, function (response, status, jqXHR) {
            if (jqXHR.getResponseHeader("is-valid") == "false") {
                $("#taskIndexView").html(response);
            }
            else {
                $("#taskIndexView").empty();
                window.location.replace(baseUrl);
            }
        }, function (jqXHR, textStatus, errorThrown) {
            console.log('POST error: ', textStatus, errorThrown, jqXHR)
        })

        

        
    });

   

    $(document).on('click', "#editSubmit", function () {
        var url = $(this).data('url');

        
        var model = {
            AssignmentID: $('#assignmentEditID').val(),
            AssignmentName: $('#assignmentEditName').val(),
            AssignmentDescription: $('#assignmentEditDescription').val(),
            DueDate: new Date($('#assignmentEditDueDate').val()).toISOString(),
            Completed: $("#assignmentEditComplete").is(":checked")
        }
        console.log(model)

        AJAXRequest('POST', url, model, { "X-Requested-With": "XMLHttpRequest" }, function (result, status, jqXHR) {
            if (jqXHR.getResponseHeader("is-valid") == "false") {
                $("#taskIndexView").html(result);
            }
            else {
                $("#taskIndexView").empty();
                window.location.replace(baseUrl);
            }
        }, function (jqXHR, textStatus, errorThrown) {
            console.log('POST error: ', textStatus, errorThrown, jqXHR)
        });
        



    });

    function displayCompletedStatusNotification(isCompleted)
    {

        console.log("Completed:", isCompleted)
        var completedNotification = $('.completed-notification')
        if (isCompleted) {
            console.log("Before add class", completedNotification)
            completedNotification.addClass("completed-notification-active");
            console.log("After add class", completedNotification)
            completedNotification.html("Task added to completed list");
        }
        else { 
            console.log("Before add class", completedNotification)
            completedNotification.addClass("completed-notification-active");
            console.log("After add class", completedNotification)
            completedNotification.html("Task removed from completed list");
        }

        setTimeout(function () {
            completedNotification.removeClass("completed-notification-active");
        }, 1500)
    }

    $(document).on("change", "#assignmentEditComplete", function () {
        console.log("checkbox changed")
        
        var isChecked = $(this).is(":checked");
        

        var taskId = $(this).data("id");
        
        var tasks = $('.task-anchor')
        var currentTask = null;
        var backupExists = false;
        

        /**
         * TODO: replace task if element is on the same page when it was removed
         * 
         * */

        $(tasks).each(function (pos, card)
        {
            console.log(card);
            if ($(card).find("#assignmentDetailId").data("id") == taskId)
            {
                currentTask = card;
                return false;
            }


        })
        
            var model = { id: taskId, completed: isChecked };
        
            AJAXRequest('POST', '/assignment/UpdateAssignmentCompletion', model, null, function (result, status, jqXHR) {
                $(currentTask).toggleClass("cd-clicked");
                $(currentTask).on('transitionend webkitTransitionEnd oTransitionEnd otransitionend MSTransitionEnd', function () {
                    console.log("transition ended");
                    console.log("removing element in AJAX");
                    $(this).parent().remove();
                });
                $("#assignmentEditComplete").attr("disabled", "disabled");
                displayCompletedStatusNotification(isChecked);

                
                console.log("task updated successfully")

                setTimeout(function () {
                    $('#taskIndexView').empty();
                }, 2000)

            }, function (jqXHR, textStatus, errorThrown) {
                console.log("error updating task", jqXHR, textStatus, errorThrown)
            })
        

        console.log(currentTask);

    })

    /**
     * Issue: ListDisplay incorrectly when sort is clicked
     * */

    const sortSwitch = {
        "1": (value, list, parent) => {
            list.sort(function (a, b) { return getTargetElement(b, value).localeCompare(getTargetElement(a, value)); })
            console.log("Parent element:", parent)
            console.log("initial List parent:", list.parent())
            
            parent.empty().append(list);
            
        },
        "2": (value, list, parent) => {
            list.sort(function (a, b) { return getTargetElement(a, value).localeCompare(getTargetElement(b, value)) })
            console.log("Parent element:", parent)
            console.log("initial List parent:", list.parent())
            
            parent.empty().append(list);
            
        },
        "3": (value, list, parent) => {
            list.sort(function (a, b) { return getTargetElement(b, value).localeCompare(getTargetElement(a, value)) })
            console.log("Parent element:", parent)
            console.log("initial List parent:", list.parent())
            
            parent.empty().append(list);
            
        },
        "default": console.log("Unknown")
    }

    $(".task-sort").on("change", function () {
        const value = $(this).val();

        const row = $('.task-row')
        var cards = row.find('.task-col-holder');
        console.log(cards)
        sortSwitch[value](value, cards, row);
        

        

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



    //Changes card color if Active
    $(".cd").on("click", function () {
        $('.cd').removeClass('active');
        $(this).addClass('active');
    });
})

