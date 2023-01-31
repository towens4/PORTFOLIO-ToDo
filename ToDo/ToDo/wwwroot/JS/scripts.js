
//var sort_by_date = function (a, b) { return getTargetElement(a, value).localeCompare(getTargetElement(b, value)); }

const sortSwitch = {
    "1": (value, list) => {
        list.sort(function (a, b) { return getTargetElement(a, value).localeCompare(getTargetElement(b, value)); })
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

const demo = {
    "value": (value) => { },
    "loe": (value) => { },
}
/** Write code to get class name of the i (icon) tag */



$('.fa-arrow-down').on("click", function () {
    $(this).toggleClass('fa-solid fa-angle-up');
})

$('.task-holder').on("click", function () {
    
    console.log(this.firstElementChild.children);
    //$(this).find('i').removeClass();
    //$(this).find('i').toggleClass("fa-solid fa-angle-down");
    toggleAngle($(this).find('i'));
    $('.task-desc', this).toggle();
});

/*function () {
    return $(this).parent().is("fa-angle-up") ? "fa-angle-down" : "fa-angle-up"
}*/

$(".task-sort").on("change", function () {
    const value = $(this).val();
    var list = $('.task-holder').get();

    sortSwitch[value](value, list) || sortSwitch["default"]();
    
});

function toggleAngle(element)
{
    if (element.hasClass("fa-solid fa-angle-up"))
    {
        element.removeClass("fa-angle-up");
        element.addClass("fa-solid fa-angle-down");
        return;
    }
    if (element.hasClass("fa-solid fa-angle-down"))
    {
        element.removeClass("fa-angle-down");
        element.addClass("fa-solid fa-angle-up");
        return;
    }
}

function getTargetElement(element, value)
{
    const index = value == 0 ? 1 : 0;
    console.log(index)
    return element.firstElementChild.children[index].innerHTML;
}

function rearrageHTML(elementList)
{
    for (var i = 0; i < elementList.length - 1; i++)
    {
        elementList[i].parentNode.appendChild(elementList[i]);
    }
}