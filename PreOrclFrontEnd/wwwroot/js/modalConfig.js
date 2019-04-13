function modalShow(jsonItem) {
    onUnLoadLoadingIcon();
    $(jsonItem.NameModalPartial).html("");
    if (jQuery.type(jsonItem.Id)  === 'undefined') {
        $.get(jsonItem.Url)
            .done(function (html) {
                $(jsonItem.NameModalPartial).html(html);
                onLoadLoadingIcon();
            });
    } else {
        $.get(jsonItem.Url, { id: jsonItem.Id })
            .done(function (html) {
                $(jsonItem.NameModalPartial).html(html);
                onLoadLoadingIcon();
            });
    }
}