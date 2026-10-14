// Legacy catalog interactivity: AJAX filter -> replaces #product-grid with a Razor partial.
$(function () {
    $('#filter-form').on('submit', function (e) {
        e.preventDefault();
        var $form = $(this);
        $.ajax({
            url: $form.attr('action'),
            type: 'GET',
            data: $form.serialize(),
            success: function (html) {
                $('#product-grid').html(html);
            },
            error: function () {
                alert('Could not load products. Please try again.');
            }
        });
    });

    // Add-to-cart handling now lives in cart.js (Shopping Cart slice).
});
