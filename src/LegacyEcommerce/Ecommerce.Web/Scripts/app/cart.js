// Cart interactions: add-to-cart (AJAX), mini-cart refresh, cart-page qty/remove.
$(function () {
    function antiForgeryToken() {
        return $('input[name="__RequestVerificationToken"]').first().val();
    }

    // Add to cart from listing cards or the detail page
    $(document).on('click', '.add-to-cart-btn', function () {
        var productId = $(this).data('product-id');
        $.ajax({
            url: '/Cart/AddToCart',
            type: 'POST',
            data: { productId: productId, quantity: 1, __RequestVerificationToken: antiForgeryToken() },
            success: function (html) {
                $('#mini-cart-container').html(html);
            },
            error: function () { alert('Could not add to cart.'); }
        });
    });

    // Cart page: change quantity
    $(document).on('change', '.cart-qty', function () {
        var $row = $(this).closest('tr');
        var productId = $row.data('product-id');
        var quantity = parseInt($(this).val(), 10);
        $.ajax({
            url: '/Cart/UpdateQuantity',
            type: 'POST',
            data: { productId: productId, quantity: quantity, __RequestVerificationToken: antiForgeryToken() },
            success: function (html) { $('#cart-table').html(html); }
        });
    });

    // Cart page: remove line
    $(document).on('click', '.cart-remove', function () {
        var $row = $(this).closest('tr');
        var productId = $row.data('product-id');
        $.ajax({
            url: '/Cart/RemoveItem',
            type: 'POST',
            data: { productId: productId, __RequestVerificationToken: antiForgeryToken() },
            success: function (html) { $('#cart-table').html(html); }
        });
    });
});
