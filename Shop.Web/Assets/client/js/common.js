var common = {
    init: function () {
        common.registerEvents();
    },
    // cài package manager nuget, sau đó cài theme của jquery ui
    registerEvents: function () {
        $("#txtSearch").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/Product/GetListProductByName",
                    dataType: "json",
                    method:"GET",
                    data: {
                        keyword: request.term
                    },
                    success: function (data) {
                        response(data);
                        console.log(data);
                    }
                });
            },
            minLength: 0,
            focus: function (event, ui) {
                $("#txtSearch").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtSearch").val(ui.item.label);
                return false;
            }
        });
    }
}
common.init();