/// <reference path="../scripts/jquery-3.4.1.min.js" />

$(document).ready(function () {
    var nrOfUserComments = 0;
    var currentlyEditingId;
    var editedComment;
    var AuthenticatedUser = $("#UserKey").attr("value");
    var submissionId = $("#parent").val();
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
            },
            error: function (xhxr, textStatus, errorThrown) {
            }
        });
    });
    $(document).on("click", ".reply", function () {
        $("#parent").val(this.id);
        $("#parent").removeClass("submissionParent");
        $("#MakeComment").modal("toggle");
    });
    $(document).on("click", ".addComment", function () {
        $("#parent").val(this.id);
        $("#parent").addClass("submissionParent");   
        $("#MakeComment").modal("toggle");
    });
    $(document).on("click", ".edit", function () {
        editedComment = $(this).parent().find(".commentText");
        $("#MakeEdit").modal("toggle");
        $("#EditContent").val( $(this).parent().find(".commentText").text());
        currentlyEditingId = this.id;

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

            },
            error: function (data) {

            }
        });


    });
    $(document).on("click", ".delete", function () {
        var id = this.id; //id of comment to be deleted
        $.ajax({
            url: "/api/Comment/"+id,
            type: "DELETE",
            success: function (data) {
                console.log("delete successful");
            },
            error: function (data) {
                console.log("delete failed");
            }

        })
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
                            <form method="post" action="" class="btn">
                                <input type="image" class="img-fluid" src="/Static/upvote-unpressed.png" alt="Upvote" />
                                <input type="image" class="img-fluid" src="/Static/downvote-unpressed.png" alt="Downvote" />
                            </form>
                        </div>
                    </div>
                    <div class="col comment-content">
                        <h5 class="card-subtitle">`+comment.Text+`</h5>
                        <h7 class="card-subtitle">Submitted by `+$("#userName").attr("value")+`</h7>
                            <button class="btn btn-dark reply" id="`+commentId+`">Reply</button>

                            <button class="btn btn-dark edit" id="`+commentId+`">Edit</button>
                            <button class="btn btn-dark delete">Delete</button>
                        
                    </div>
                </div>

            </div>

        </div>
       `
    $("#CommentsStart").after(htmlElement);

}
