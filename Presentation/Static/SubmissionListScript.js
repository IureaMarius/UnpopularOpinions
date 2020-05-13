/// <reference path="../scripts/jquery-3.4.1.min.js" />

$(document).ready(function () {

    var currentlyEditingId;
    var editedSubmissionTitle;
    var editedSubmissionContent;
    var AuthenticatedUser = $("#UserKey").attr("value");


    $(document).on("click", ".upvoteSubmission", function () {
        if ($("#UserKey").length) {
            var voteValue;
            if ($(this).hasClass("unpressed")) {
                //press it
                voteValue = 1;
                $(this).attr("src", "/Static/upvote-pressed.png");
                $(this).parent().find(".downvoteSubmission").attr("src", "/Static/downvote-unpressed.png");
                $(this).removeClass("unpressed");
                $(this).addClass("pressed");
                $(this).parent().find(".downvoteSubmission").removeClass("pressed");
                $(this).parent().find(".downvoteSubmission").addClass("unpressed");
            } else {
                voteValue = 0;
                $(this).attr("src", "/Static/upvote-unpressed.png");
                $(this).parent().find(".downvoteSubmission").attr("src", "/Static/downvote-unpressed.png");
                $(this).removeClass("pressed");
                $(this).addClass("unpressed");
                $(this).parent().find(".downvoteSubmission").removeClass("pressed");
                $(this).parent().find(".downvoteSubmission").addClass("unpressed");
                //unpress it

            }

            var VoteViewModel = new Object();
            VoteViewModel.SubOrCommId = this.id;
            VoteViewModel.VoteValue = voteValue;
            VoteViewModel.VoteType = 1;
            VoteViewModel.VoterId = AuthenticatedUser
            $.ajax({
                url: "/api/Vote/CastSubmissionVote",
                type: "PUT",
                contentType: "Application/json",
                data: JSON.stringify(VoteViewModel),
                dataType: "json",
                success: function (data) {

                },
                error: function (data) {
                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("An error occured while processing the vote");
                    $(".toast").toast({
                        delay: 1500
                    });
                    $(".toast").css("color", "red");
                    $(".toast").toast("show");


                }

            });
        } else {

            $(".toast-title").text("Unpopular Opinions");
            $(".toast-message").text("Log in to vote!");
            $(".toast").toast({
                delay: 1500
            });
            $(".toast").css("color", "blue");
            $(".toast").toast("show");
            //tell user to log in
        }
    });

    $(document).on("click", ".downvoteSubmission", function () {

        if ($("#UserKey").length) {
            var voteValue;
            if ($(this).hasClass("unpressed")) {
                //press it
                voteValue = -1;
                $(this).attr("src", "/Static/downvote-pressed.png");
                $(this).parent().find(".upvoteSubmission").attr("src", "/Static/upvote-unpressed.png");
                $(this).removeClass("unpressed");
                $(this).addClass("pressed");
                $(this).parent().find(".upvoteSubmission").removeClass("pressed");
                $(this).parent().find(".upvoteSubmission").addClass("unpressed");
            } else {
                voteValue = 0;
                $(this).attr("src", "/Static/downvote-unpressed.png");
                $(this).parent().find(".upvoteSubmission").attr("src", "/Static/upvote-unpressed.png");
                $(this).removeClass("pressed");
                $(this).addClass("unpressed");
                $(this).parent().find(".upvoteSubmission").removeClass("pressed");
                $(this).parent().find(".upvoteSubmission").addClass("unpressed");
                //unpress it
            }

            var VoteViewModel = new Object();
            VoteViewModel.SubOrCommId = this.id;
            VoteViewModel.VoteValue = voteValue;
            VoteViewModel.VoteType = 1;
            VoteViewModel.VoterId = AuthenticatedUser
            $.ajax({
                url: "/api/Vote/CastSubmissionVote",
                type: "PUT",
                contentType: "Application/json",
                data: JSON.stringify(VoteViewModel),
                dataType: "json",
                success: function (data) {

                },
                error: function (data) {
                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("An error occured while processing the vote");
                    $(".toast").toast({
                        delay: 1500
                    });
                    $(".toast").css("color", "red");
                    $(".toast").toast("show");


                }

            });
        } else {

            $(".toast-title").text("Unpopular Opinions");
            $(".toast-message").text("Log in to vote!");
            $(".toast").toast({
                delay: 1500
            });
            $(".toast").css("color", "blue");
            $(".toast").toast("show");
            //tell user to log in
        }
    });

    $(document).on("click", ".edit", function () {
        editedSubmissionTitle = $(this).parent().find(".submission-title");
        editedSubmissionContent = $(this).parent().find(".submission-content");
        $("#MakeEdit").modal("toggle");
        $("#EditTitle").focus();
        $("#EditContent").val(editedSubmissionContent.text());
        $("#EditTitle").val(editedSubmissionTitle.text());
        currentlyEditingId = this.id;

    });

    $(document).on("click", "#put-button", function () {
        var EditSubmissionViewModel = new Object();
        EditSubmissionViewModel.AuthorId = AuthenticatedUser;
        EditSubmissionViewModel.SubmissionId = currentlyEditingId;
        EditSubmissionViewModel.Content = $("#EditContent").val();
        EditSubmissionViewModel.Title = $("#EditTitle").val();
        $.ajax({
            url: "/api/Submission",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(EditSubmissionViewModel),
            dataType: "json",
            success: function (data) {

                editedSubmissionContent.text($("#EditContent").val());
                editedSubmissionTitle.text($("#EditTitle").val());
                $("#EditContent").val("");
                $("#MakeEdit").modal("toggle");

                $(".toast-title").text("Unpopular Opinions");
                $(".toast-message").text("Edit successful!");
                $(".toast").toast({
                    delay: 1500
                });
                $(".toast").css("color", "green");
                $(".toast").toast("show");
            },
            error: function (data) {

                $(".toast-title").text("Unpopular Opinions");
                $(".toast-message").text("There was an error proccessing your edit.");
                $(".toast").toast({
                    delay: 1500
                });
                $(".toast").css("color", "red");
                $(".toast").toast("show");
            }
        });

    });

    $(document).on("click", ".delete", function () {
        var id = this.id; //id of comment to be deleted
        var clicked = $(this);
        $.ajax({
            url: "/api/Submission/" + id,
            type: "DELETE",
            success: function (data) {
                $(".toast-title").text("Unpopular Opinions");
                $(".toast-message").text("Delete successful.");
                $(".toast").toast({
                    delay: 1500
                });
                $(".toast").css("color", "green");
                $(".toast").toast("show");
                clicked.closest(".card").remove();

            },
            error: function (data) {
                $(".toast-title").text("Unpopular Opinions");
                $(".toast-message").text("There was an error while proccessing your delete request.");
                $(".toast").toast({
                    delay: 1500
                });
                $(".toast").css("color", "red");
                $(".toast").toast("show");
            }

        });
    });
});
