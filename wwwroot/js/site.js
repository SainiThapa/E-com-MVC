    // Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
    // for details on configuring this project to bundle and minify static web assets.

    // Write your JavaScript code.

    document.addEventListener('DOMContentLoaded', function () {
        const quantityInput = document.getElementById('productQuantity');
        const cartQuantityInput = document.getElementById('cartQuantity');
    
        if (quantityInput && cartQuantityInput) {
            quantityInput.addEventListener('input', function () {
                const quantity = this.value;
                cartQuantityInput.value = quantity;
            });
        }
    });