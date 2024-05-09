
document.addEventListener("DOMContentLoaded", function () {
    var sidebarCollapse = document.getElementById("sidebarCollapse");
    var sidebar = document.getElementById("sidebar");

    sidebarCollapse.addEventListener("click", function() {
        sidebar.classList.toggle("active");
    });
        
    var editPostButtons = document.querySelectorAll(".edit-post");
    editPostButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            var postId = button.dataset.postId;
            var postContent = button.closest(".container").querySelector("#currentpost .card-text").textContent.trim();
            document.getElementById("postContent").value = postContent;
            document.getElementById("postId").value = postId;
            document.getElementById("editPostModal").classList.add("show");
            document.getElementById("editPostModal").style.display = "block";
            document.querySelector(".background-container").classList.add("blur");
        });
    });


    document.getElementById("savePostBtn")?.addEventListener("click", function () {
        var postId = document.getElementById("postId").value;
        var editedContent = document.getElementById("postContent").value;
    
        fetch("/Posts/Edit", {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded"
            },
            body: "id=" + encodeURIComponent(postId) + "&content=" + encodeURIComponent(editedContent)
        })
        .then(response => {
            if (response.ok) {
                var postTextElement = document.querySelector(".edit-post[data-post-id='" + postId + "']").closest(".container").querySelector("#currentpost .card-text");
                postTextElement.textContent = editedContent;
                document.getElementById("editPostModal").classList.remove("show");
                document.getElementById("editPostModal").style.display = "none";
                document.querySelector(".background-container").classList.remove("blur");
            } else {
                console.error("Error:", response.statusText);
            }
        })
        .catch(error => {
            console.error("Error:", error);
        });
    });
    
    document.querySelectorAll("[data-dismiss='modal']")?.forEach(function (button) {
        button.addEventListener("click", function () {
            document.getElementById("editPostModal").classList.remove("show");
            document.getElementById("editPostModal").style.display = "none";
            // Remove blur effect from background
            document.querySelector(".background-container").classList.remove("blur");
        });
    });




    var likeButtons = document.querySelectorAll('.fa-heart');

    likeButtons.forEach(function(likeButton) {
        likeButton.addEventListener('click', function() {
            var postId = likeButton.dataset.id;
            fetch("/Likes/LikePost", {
                method: "POST",
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded",
                },
                body: "postId=" + encodeURIComponent(postId)
            })
            .then(response => {
                if (response.ok) {
                    return response.json(); // Parse JSON response
                } else {
                    throw new Error('Network response was not ok.');
                }
            })
            .then(data => {
                if (data.liked) {
                    likeButton.classList.add('fa-solid');
                    likeButton.classList.remove("fa-regular");
                    var likeCountElement = document.getElementById(`likeCount_${postId}`);
                    likeCountElement.textContent = parseInt(likeCountElement.textContent) + 1;
                } else {
                    likeButton.classList.remove('fa-solid');
                    likeButton.classList.add("fa-regular");
                    var likeCountElement = document.getElementById(`likeCount_${postId}`);
                    likeCountElement.textContent = parseInt(likeCountElement.textContent) -1;
                }
            })
            .catch(error => {
                console.error("Error: ", error);
            });
        });
    });
    


});
