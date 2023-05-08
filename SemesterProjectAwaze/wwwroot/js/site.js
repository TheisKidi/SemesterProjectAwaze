// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


        /* When the user clicks on the button,
toggle between hiding and showing the dropdown content */
function myFunction() {
    document.getElementById("place").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}


document.getElementById('Price').addEventListener('input', function (event) {
    document.getElementById('priceValue').textContent = event.target.value;
});

document.getElementById('stjerner').addEventListener('input', function (event) {
    document.getElementById('stjernerValue').textContent = event.target.value;
});

document.getElementById('soveværelser').addEventListener('input', function (event) {
    document.getElementById('soveværelserValue').textContent = event.target.value;
});

document.getElementById('badeværelser').addEventListener('input', function (event) {
    document.getElementById('badeværelserValue').textContent = event.target.value;
});
