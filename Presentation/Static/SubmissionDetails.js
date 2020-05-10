/// <reference path="../scripts/jquery-3.4.1.min.js" />

$(document).ready(function () {
    var nrOfUserComments = 0;
    var submissionId = $("#parent").val();
    $("#post-button").click(function () {
        var comment = new Object();
        comment.Text = $("#Content").val();
        comment.AuthorId = ($("#userId").attr("content"));
        if ($("#parent").attr("class") == "submissionParent") {
            comment.ParentSubmissionId = $("#parent").val();
        }
        else
            comment.ParentCommentId = $("#parent").val();

        $.ajax({
            url: "/api/Comment",
            type: "POST",
            contentType: "Application/json",
            data: JSON.stringify(comment),
            dataType: "json",
            success: function (data, textStatus, xhr) {
                $("#Content").val("");
                $("#MakeComment").modal("toggle");
                $("#parent").addClass("submissionParent")
                $("#parent").val(submissionId);
            },
            error: function (xhxr, textStatus, errorThrown) {
            }
        });
    });

    $(".reply").click(function () {
        $("#parent").val(this.id);
        $("#parent").removeClass("submissionParent");
        $("#MakeComment").modal("toggle");
    });

});

function AddUserComment(comment) {
    var htmlElement =` 
    <div class='card'>
            <div class='container'>
                <div class='row'>
                    <div class='col-1'>
                        <div class='btn-group-vertical'>
                            <form method='post' action='' class='btn'>
                                <input type='image' class='img-fluid' src='~/Static/upvote-unpressed.png' alt='Upvote' />
                                <input type='image' class='img-fluid' src='~/Static/downvote-unpressed.png' alt='Downvote' />
                            </form>
                        </div>
                    </div>
                    <div class='col'>
                        <h5 class='card-subtitle'>comment.Text</h5>
                        <h7 class='card-subtitle'>Submitted by comment.AuthorName</h7>
                    </div>
                </div>

            </div>

</div>
       ` 

}
