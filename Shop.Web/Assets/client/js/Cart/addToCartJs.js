var cart = {
    init: function () {
        cart.registerEvent();
    },
    registerEvent: function () {
        $('.btnAddToCart').off('click').on('click', function (e) {
            e.preventDefault();
            var id = parseInt($(this).data('id'));
            cart.addItem(id);
        });
    },
    addItem: function (productId) {
        $.ajax({
            url: '/ShoppingCart/Add',
            type: 'POST',
            dataType: 'Json',
            data: {
                productId: productId
            },
            success: function (response) {
                if (response.status) {
                    alert("Đã thêm sản phẩm vào giỏ hàng");
                }
            }
        });
    }

}
cart.init();