/// <reference path="../scripts/jquery-3.4.1.min.js" />

$(document).ready(function () {
    var nrOfUserComments = 0;
    var currentlyEditingId;
    var editedComment;
    var AuthenticatedUser = $("#UserKey").attr("value");
    var submissionId = $("#parent").val();
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
                        delay:1500
                    });
                    $(".toast").css("color", "red");
                    $(".toast").toast("show");


                }

            });
        } else {

                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("Log in to vote!");
                    $(".toast").toast({
                        delay:1500
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
                        delay:1500
                    });
                    $(".toast").css("color", "red");
                    $(".toast").toast("show");


                }

            });
        } else {

                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("Log in to vote!");
                    $(".toast").toast({
                        delay:1500
                    });
                    $(".toast").css("color", "blue");
                    $(".toast").toast("show");
            //tell user to log in
        }
    });
    $(document).on("click", ".upvoteComment", function () {

        if ($("#UserKey").length) {
            var voteValue;
            if ($(this).hasClass("unpressed")) {
                //press it
                voteValue = 1;
                $(this).attr("src", "/Static/upvote-pressed.png");
                $(this).parent().find(".downvoteComment").attr("src", "/Static/downvote-unpressed.png");
                $(this).removeClass("unpressed");
                $(this).addClass("pressed");
                $(this).parent().find(".downvoteComment").removeClass("pressed");
                $(this).parent().find(".downvoteComment").addClass("unpressed");
            } else {
                voteValue = 0;
                $(this).attr("src", "/Static/upvote-unpressed.png");
                $(this).parent().find(".downvoteComment").attr("src", "/Static/downvote-unpressed.png");
                $(this).removeClass("pressed");
                $(this).addClass("unpressed");
                $(this).parent().find(".downvoteComment").removeClass("pressed");
                $(this).parent().find(".downvoteComment").addClass("unpressed");
                //unpress it
                
            }

            var VoteViewModel = new Object();
            VoteViewModel.SubOrCommId = this.id;
            VoteViewModel.VoteValue = voteValue;
            VoteViewModel.VoteType = 0;
            VoteViewModel.VoterId = AuthenticatedUser
            $.ajax({
                url: "/api/Vote/CastCommentVote",
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
                        delay:1500
                    });
                    $(".toast").css("color", "red");
                    $(".toast").toast("show");


                }

            });
        } else {

                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("Log in to vote!");
                    $(".toast").toast({
                        delay:1500
                    });
                    $(".toast").css("color", "blue");
                    $(".toast").toast("show");
            //tell user to log in
        }
    });
    $(document).on("click", ".downvoteComment", function () {
        if ($("#UserKey").length) {
            var voteValue;
            if ($(this).hasClass("unpressed")) {
                //press it
                voteValue = -1;
                $(this).attr("src", "/Static/downvote-pressed.png");
                $(this).parent().find(".upvoteComment").attr("src", "/Static/upvote-unpressed.png");
                $(this).removeClass("unpressed");
                $(this).addClass("pressed");
                $(this).parent().find(".upvoteComment").removeClass("pressed");
                $(this).parent().find(".upvoteComment").addClass("unpressed");
            } else {
                voteValue = 0;
                $(this).attr("src", "/Static/downvote-unpressed.png");
                $(this).parent().find(".upvoteComment").attr("src", "/Static/upvote-unpressed.png");
                $(this).removeClass("pressed");
                $(this).addClass("unpressed");
                $(this).parent().find(".upvoteComment").removeClass("pressed");
                $(this).parent().find(".upvoteComment").addClass("unpressed");
                //unpress it
            }

            var VoteViewModel = new Object();
            VoteViewModel.SubOrCommId = this.id;
            VoteViewModel.VoteValue = voteValue;
            VoteViewModel.VoteType = 0;
            VoteViewModel.VoterId = AuthenticatedUser
            $.ajax({
                url: "/api/Vote/CastCommentVote",
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
                        delay:1500
                    });
                    $(".toast").css("color", "red");
                    $(".toast").toast("show");


                }

            });
        } else {

                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("Log in to vote!");
                    $(".toast").toast({
                        delay:1500
                    });
                    $(".toast").css("color", "blue");
                    $(".toast").toast("show");
            //tell user to log in
        }
    });
    $(document).on("click","#post-button",function () {
        var comment = new Object();
        comment.Text = $("#Content").val();
        comment.AuthorId = AuthenticatedUser;
        if ($("#parent").attr("class") == "submissionParent") {
            comment.ParentSubmissionId = $("#parent").val();
            comment.ParentCommentId = null;
        }
        else {
            comment.ParentCommentId = $("#parent").val();
            comment.ParentSubmissionId = null;
        }
        $.ajax({
            url: "/api/Comment",
            type: "POST",
            contentType: "Application/json",
            data: JSON.stringify(comment),
            dataType: "json",
            success: function (data) {
                $("#Content").val("");
                $("#MakeComment").modal("toggle");
                $("#parent").addClass("submissionParent")
                $("#parent").val(submissionId);
                if (comment.ParentCommentId == null)
                    AddUserComment(comment,data);

                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("Comment sent successfully!");
                    $(".toast").toast({
                        delay:1500
                    });
                    $(".toast").css("color", "green");
                    $(".toast").toast("show");

            },
            error: function (xhxr, textStatus, errorThrown) {

                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("There was an error proccessing your comment.");
                    $(".toast").toast({
                        delay:1500
                    });
                    $(".toast").css("color", "red");
                    $(".toast").toast("show");

            }
        });
    });
    $(document).on("click", ".reply", function () {
        $("#parent").val(this.id);
        $("#parent").removeClass("submissionParent");
        $("#MakeComment").modal("toggle");
        $("#Content").focus();
    });
    $(document).on("click", ".addComment", function () {
        $("#parent").val(this.id);
        $("#parent").addClass("submissionParent");   
        $("#MakeComment").modal("toggle");
        $("#Content").focus();
    });
    $(document).on("click", ".edit", function () {
        editedComment = $(this).parent().find(".commentText");
        $("#MakeEdit").modal("toggle");
        $("#EditContent").val( $(this).parent().find(".commentText").text());
        currentlyEditingId = this.id;
        $("#EditContent").focus();

       });

    $(document).on("click", "#put-button", function () {
        var EditCommentViewModel = new Object();
        EditCommentViewModel.AuthorId = AuthenticatedUser;
        EditCommentViewModel.commentId = currentlyEditingId;
        EditCommentViewModel.Text = $("#EditContent").val();
        $.ajax({
            url: "/api/Comment",
            type: "PUT",
            contentType: "application/json",
            data: JSON.stringify(EditCommentViewModel),
            dataType: "json",
            success: function (data) {

                editedComment.text($("#EditContent").val());
                $("#EditContent").val("");
                $("#MakeEdit").modal("toggle");

                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("Edit successful!");
                    $(".toast").toast({
                        delay:1500
                    });
                    $(".toast").css("color", "green");
                    $(".toast").toast("show");
            },
            error: function (data) {

                    $(".toast-title").text("Unpopular Opinions");
                    $(".toast-message").text("There was an error proccessing your edit.");
                    $(".toast").toast({
                        delay:1500
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
            url: "/api/Comment/" + id,
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

function AddUserComment(comment, commentId) {
    var htmlElement =
        ` 

        <div class="card AuthorIdHolder" id="`+comment.AuthorId+`" >
            <div class="container">

                <div class="row">
                    <div class="col-1">
                        <div class="btn-group-vertical">
                                <input type="image" class="img-fluid upvoteComment unpressed" id="`+commentId+`" src="/Static/upvote-unpressed.png" alt="Upvote" />
                                <input type="image" class="img-fluid downvoteComment unpressed" id="`+commentId+`" src="/Static/downvote-unpressed.png" alt="Downvote" />
                        </div>
                    </div>
                    <div class="col comment-content">
                        <h5 class="card-subtitle">`+ comment.Text + `</h5>
                        <h7 class="card-subtitle" onclick="window.location.href ='/User/UserSubmissions/`+ comment.AuthorId + `/0 '">Submitted by ` + $("#userName").attr("value") +`</h7>
                            <button class="btn btn-dark reply" id="`+commentId+`">Reply</button>

                            <button class="btn btn-dark edit" id="`+commentId+`">Edit</button>
                            <button class="btn btn-dark delete" id="`+commentId+`">Delete</button>
                        
                    </div>
                </div>

            </div>

        </div>
       `
    $("#CommentsStart").after(htmlElement);

}
