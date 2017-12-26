//Service => data service that is responsable for calling the server. This can be resused to views/controllers
var FollowingService = function () {
    //Manager methods => that is purely responsible for making decisions & these methods below are about data access
    var createFollowing = function (followeeId, done, fail) {
        $.post("/api/followings/", { followeeId: followeeId })
        .done(done)
        .fail(fail);
    };

    var deleteFollowing = function (followeeId, done, fail) {
        $.ajax({
            url: "/api/followings/" + followeeId,
            method: "DELETE"
        })
                .done(done)
                .fail(fail);
    };
    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }
}();