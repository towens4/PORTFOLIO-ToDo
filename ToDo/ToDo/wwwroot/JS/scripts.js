
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

$('.task-holder').on("click", function () {
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