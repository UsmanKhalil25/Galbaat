// editPostModal.js

document.addEventListener("DOMContentLoaded", function () {
    // Get all edit post buttons
    var editPostButtons = document.querySelectorAll(".edit-post");

    // Add click event listener to each edit post button
    editPostButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            // Get post id and content
            var postId = button.dataset.postId;
            var postContent = button.closest(".container").querySelector("#currentpost .card-text").textContent.trim();

            // Pre-fill modal fields
            document.getElementById("postContent").value = postContent;
            document.getElementById("postId").value = postId;

            // Show the modal
            document.getElementById("editPostModal").classList.add("show");
            document.getElementById("editPostModal").style.display = "block";

            // Add blur effect to background only
            document.querySelector(".background-container").classList.add("blur");
        });
    });


    document.getElementById("savePostBtn").addEventListener("click", function () {
        var postId = document.getElementById("postId").value;
        var editedContent = document.getElementById("postContent").value;
    
        // Perform fetch request to update post content
        fetch("/Posts/Edit", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: "id=" + encodeURIComponent(postId) + "&content=" + encodeURIComponent(editedContent)
        })
        .then(response => {
            if (response.ok) {
                // On success, update the post content on the page
                var postTextElement = document.querySelector(".edit-post[data-post-id='" + postId + "']").closest(".container").querySelector("#currentpost .card-text");
                postTextElement.textContent = editedContent;
                // Hide the modal
                document.getElementById("editPostModal").classList.remove("show");
                document.getElementById("editPostModal").style.display = "none";
                // Remove blur effect from background
                document.querySelector(".background-container").classList.remove("blur");
            } else {
                console.error("Error:", response.statusText);
                // Handle error if needed
            }
        })
        .catch(error => {
            console.error("Error:", error);
            // Handle error if needed
        });
    });
    
    // Add click event listener to close modal button
    document.querySelectorAll("[data-dismiss='modal']").forEach(function (button) {
        button.addEventListener("click", function () {
            document.getElementById("editPostModal").classList.remove("show");
            document.getElementById("editPostModal").style.display = "none";
            // Remove blur effect from background
            document.querySelector(".background-container").classList.remove("blur");
        });
    });
});
