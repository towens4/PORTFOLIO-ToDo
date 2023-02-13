
//var sort_by_date = function (a, b) { return getTargetElement(a, value).localeCompare(getTargetElement(b, value)); }

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


$('.fa-arrow-down').on("click", function () {
    $(this).toggleClass('fa-solid fa-angle-up');
})

$('.task-holder').on("click", function () {
    const icon = $(this).find('i').attr("class");
    toggleAngle[icon]($(this).find('i'));
    $('.task-desc', this).toggle();
});

$(".task-sort").on("change", function () {
    const value = $(this).val();
    var list = $('.task-holder').get();

    sortSwitch[value](value, list) || sortSwitch["default"]();
    
});

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