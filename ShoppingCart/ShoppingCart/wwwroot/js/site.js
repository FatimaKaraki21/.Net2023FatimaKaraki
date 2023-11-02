$(function () {

    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(() => {
            if (!confirm("Confirm deletion")) return false;
        });
    }

    if ($("div.alert.notification").length) {
        setTimeout(() => {
            $("div.alert.notification").fadeOut();
        }, 2000);
    }

});

function readURL(input) {
    if (input.files && input.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $("img#imgpreview").attr("src", e.target.result).width(200).height(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

document.addEventListener("DOMContentLoaded", function () {
    animateText();
});

function animateText() {
    var text = "Welcome To Your Favorite Shopping Cart";
    var el = document.getElementById("animate-text");
    el.innerHTML = ""; // Clear the initial text

    // Split the text into individual characters and display them one by one
    for (var i = 0; i < text.length; i++) {
        (function (i) {
            setTimeout(function () {
                el.innerHTML += text.charAt(i);
                if (i === text.length - 1) {
                    showAdditionalText();
                }
            }, i * 60); // Adjust the timing as needed
        })(i);
        
    }
    
}
function showAdditionalText() {
    var additionalText = document.getElementById("additional-text");
    additionalText.style.display = "block";
}
