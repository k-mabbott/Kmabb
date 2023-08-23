// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// ----------------------------------- Home Page Animations

let sections = document.querySelectorAll('section');

window.onscroll = () => {
    sections.forEach(sec => {
        let top = window.scrollY;
        let offset = sec.offsetTop;
        let height = sec.offsetHeight;
        
        if (top <= offset && top <= offset - height / 2) {
            sec.classList.add('show-animate');
        }
        else {
            sec.classList.remove('show-animate');
        }
        
        
    })
    scrollFunction()
}

// ----------------------------------- Gallery Scroll to top

let topBtn = document.getElementById("topBtn");

// window.onscroll = () => { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 200 || document.documentElement.scrollTop > 200) {
        topBtn.style.display = "block";
    } else {
        topBtn.style.display = "none";
    }
}

function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

// ----------------------------------- Gallery Popup Modal

// Get the modal
var modal = document.getElementById("myModal");

// Get the image and insert it inside the modal - use its "alt" text as a caption
let imgs = document.querySelectorAll(".card-img-top");
var modalImg = document.getElementById("img01");

imgs.forEach( img => {
    img.onclick = function () {
        console.log("yup")
        modal.style.display = "block";
        modalImg.src = this.src;
    }

})


// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}