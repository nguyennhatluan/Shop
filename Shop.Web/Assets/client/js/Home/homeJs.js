var home = {
    init: function () {
        home.registerEvent();
    },
    registerEvent: function () {
        $('#btnSearch').off('click').on('click', function () {
            var page = 1;
            var size = 5;
            var search = $('#txtSearch').val();
            $.ajax({
                url: '/Product/LoadData',
                type: 'POST',
                dataType: 'Json',
                data: {
                    pageIndex: page,
                    pageSize: size,
                    strSearch:search
                },
                success: function (response) {
                    var data = response.data;
                    var html = '';
                    var template = $('#data-template').html();
                    $.each(data, function (i, item) {
                        html += Mustache.render(template, {
                            
                            Name: item.Name,
                            PromotionPrice: item.PromotionPrice,
                            Price: item.Price,
                            Image: item.Image,
                            ID: item.ID,
                            Alias: item.Alias
                        });
                    });
                    $('#productData').html(html);
                }
            });
        });
    }
}
home.init();