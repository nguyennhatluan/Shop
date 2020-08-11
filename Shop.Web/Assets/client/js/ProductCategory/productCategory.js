$("#btnSearch").click(function () {
    var txtSearch = $("#txtSearch").val();
    $.ajax({
        url: "",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        method: "Get",
        data: {
            keyword: txtSearch
        },
        success: function (result) {
            var html = "";
        }
    });
});