// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.addEventListener('DOMContentLoaded', function () {
    var popup = document.getElementById('cart-popup');
    if (popup) {
        setTimeout(function () {
            popup.style.display = 'none';
        }, 3500);
    }
});