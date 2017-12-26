//Service => data service that is responsable for calling the server. This can be resused to views/controllers
var AttendanceService = function () {
    //Manager methods => that is purely responsible for making decisions & these methods below are about data access
    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { gigId: gigId })
        .done(done)
        .fail(fail);
    };

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            method: "DELETE"
        })
                .done(done)
                .fail(fail);
    };
    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();