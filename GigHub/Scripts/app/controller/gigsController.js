//Using IIFE - (Immediately Invoked Function Expression)
//Module = > parsing in another module (attendanceService)
var GigsController = function (attendanceService) {
    var button;

    var init = function (container) {
        //1#args type of event, 2#args selector class, 3#args handler
        $(container).on("click", ".js-toggle-attendance", toggleAttendance);
    };

    var toggleAttendance = function (e) {
        button = $(e.target);

        var gigId = button.attr("data-gig-id");

        if (button.hasClass("btn-default"))
            attendanceService.createAttendance(gigId, done, fail);
        else
            attendanceService.deleteAttendance(gigId, done, fail);
    };

    var done = function () {
        var text = (button.text() == "Going") ? "Going?" : "Going";

        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };

    var fail = function () {
        alert("Something failed !");
    };

    //Return the function to make them public
    return {
        //Keys
        init: init
    }
}(AttendanceService);
