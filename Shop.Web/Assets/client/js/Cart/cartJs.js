var Cart = {
    init: function () {
        Cart.loadData();
        Cart.registerEvent();
    },
    registerEvent: function () {
        
        $('.btnDeleteItem').off('click').on('click', function (e) {
            e.preventDefault();
            var id = parseInt($(this).data('id'));
            Cart.deleteItem(id);
           
        });
        $('.txtQuantity').off('keyup').on('keyup', function () {

            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            var data = {
                Quantity: quantity,
                ProductId: productId
            };
            if (isNaN(quantity) == false) {
                $('#amount_' + productId).text(numeral(quantity * price).format('0,0'));
                setTimeout(200);
                Cart.updateItem(data);
            }
            else {
                $('#amount_' + productId).text(0);
            }

            $('#lblTotalOrder').text(numeral(Cart.getTotalOrder()).format('0,0'));
           
        });
        $(":input").bind('keyup mouseup', function () {
            var quantity = parseInt($(this).val());
            var productId = parseInt($(this).data('id'));
            var price = parseFloat($(this).data('price'));
            var data = {
                Quantity: quantity,
                ProductId: productId
            };
            if (isNaN(quantity) == false) {
                $('#amount_' + productId).text(numeral(quantity * price).format('0,0'));
                setTimeout(200);
                Cart.updateItem(data);
            }
            else {
                $('#amount_' + productId).text(0);
            }
            $('#lblTotalOrder').text(numeral(Cart.getTotalOrder()).format('0,0'));
            
        });
    },
    
    getTotalOrder: function () {
        var listQuantity = $('.txtQuantity');
        var total = 0;
        $.each(listQuantity, function (i, item) {
            total += parseInt($(item).val()) * parseFloat($(item).data('price'));
        });
        return total;
    },
    loadData: function () {
        $.ajax({
            url: '/ShoppingCart/GetAll',
            type: 'GET',
            dataType: 'Json',
            success: function (response) {
                if (response.status) {
                    var template = $('#tplCart').html();
                    var html = '';
                    var data = response.data;

                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            ProductId: item.ProductId,
                            ProductName: item.Product.Name,
                            Image: item.Product.Image,
                            Price: item.Product.Price,
                            PriceF: numeral(item.Product.Price).format('0,0'),
                            Quantity: item.Quantity,
                            Amount: numeral(item.Quantity * item.Product.Price).format('0,0')
                        });
                    });
                    $('#tb').html(html);
                    $('#lblTotalOrder').text(numeral(Cart.getTotalOrder()).format('0,0'));
                    Cart.registerEvent();
                }
            }
        });
    },
    updateItem: function (data) {
        
        $.ajax({
            url: '/ShoppingCart/Update',
            type: 'POST',
            dataType: 'Json',
            data: {
                item: JSON.stringify(data)
            },
            
            success: function () {

            }
        });
    },
    deleteItem: function (id) {
        $.ajax({
            url: '/ShoppingCart/DeleteItem',
            type: 'POST',
            dataType: 'Json',
            data: {
                productId:id
            },
            success: function (response) {
                if (response.status) {
                    alert("Đã xóa sản phẩm khỏi giỏ hàng");
                    Cart.init();
                }
                else {
                    alert("Không thể xóa sản phẩm khỏi giỏ hàng");
                }
            }
        });
    }
}
Cart.init();