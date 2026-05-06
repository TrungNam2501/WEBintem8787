
$(document).bind("keydown", function (evt) {
    var keycode = (evt.keyCode ? evt.keyCode : evt.charCode);
    //alert(keycode);
    switch (keycode) {
        case 119: //F8 key on Windows and most browsers
        case 123: //F12 key on Windows and most browsers
        case 63243:  //F8 key on Mac Safari
            evt.preventDefault();
            //Remapping event
            evt.originalEvent.keyCode = 0;
            return false;
            break;
    }
});

$('.GridViewRowStyle').hover(function () {
    $(this).css({ 'font-weight': 'bold', 'background-color': 'lavender', 'color': 'black', 'font-family': 'Arial' });
}, function () {
    $(this).css({ 'font-weight': '', 'font-size': '', 'color': 'black', 'font-family': '', 'background-color': '' })
}).end();



$(document).ready(function () {
    $(".jsQuantity").keyup(function () {
        calcu(this);
    })
})


function calcu(val) {
    var row = $(val).closest("tr");
    var price = row.find(".unpri").html();
    let cusno = $(".lbcusID").html().trim();
    var iven = row.find(".inventory").html();

    iven = iven.replace(",", "");
    iven = iven.replace(",", "");
    iven = parseFloat(iven);
    price = price.replace(",", "");
    price = price.replace(",", "");
    price = parseFloat(price);

    var quantity = parseFloat(row.find(".jsQuantity").val());
    if (isNaN(quantity)) quantity = '';

    var total = quantity * price;

    if (total == 0) {
        total = "";
    } else {
        total = parseFloat(total).toLocaleString()
    }
    row.find(".jsTotal").val(total);
    let promotion = "";
    let cls = row.find(".jscls").html().trim();

    if (cls == 'YDI') {
        if (quantity < 100) {
            promotion = promotion.toString();
            promotion = "";
            row.find(".jsPromotion").val(promotion);
        }
        if (quantity >= 100) {
            var promotion1 = quantity.toString();
            promotion1 = promotion1.substring(0, 1);
            promotion1 = parseInt(promotion1) * 5
            row.find(".jsPromotion").val(promotion1);
        }
        if (quantity >= 1000) {
            var promotion1 = quantity.toString();
            promotion1 = promotion1.substring(0, 2);
            promotion1 = parseInt(promotion1) * 5
            row.find(".jsPromotion").val(promotion1);
        }
        if (quantity >= 10000) {
            var promotion1 = quantity.toString();
            promotion1 = promotion1.substring(0, 3);
            promotion1 = parseInt(promotion1) * 5
            row.find(".jsPromotion").val(promotion1);
        }
        var promo = parseFloat(row.find(".jsPromotion").val());
        if (isNaN(promo)) promo = '';
        if (promo + quantity > iven) {
            if (confirm("Số lượng tồn không đủ! Bạn có muốn tiếp tục?")) {

            } else {
                row.find(".jsPromotion").val("");
                row.find(".jsQuantity").val("");
                row.find(".jsTotal").val("");
            }
        }
    } else if (cusno == 'V00079') {
        //let size = row.find(".jsSize").html().trim();
        //let patt = row.find(".jsPatt").html().trim();
        //let itdscCut = row.find(".jsItdscCut").html().trim();
        //let itdsc = row.find(".jsItdsc").html().trim();

        //itdsc = itdsc.replace(size, "");
        //itdsc = itdsc.replace(patt, "");
        //itdsc = itdsc.replace(itdscCut, "");
        //itdsc = itdsc.trim();
        //itdsc = itdsc.replace(" ", "");
        //itdsc = itdsc.substring(3, 2);

        let itdsc = row.find(".jsItdsc").html().trim();


        if (itdsc.indexOf("(Bạt Đen)") !== -1) {
            if (quantity < 100) {
                promotion = promotion.toString();
                promotion = "";
                row.find(".jsPromotion").val(promotion);
            }
            if (quantity >= 100) {
                var promotion1 = quantity.toString();
                promotion1 = promotion1.substring(0, 1);
                promotion1 = parseInt(promotion1) * 5
                row.find(".jsPromotion").val(promotion1);
            }
            if (quantity >= 1000) {
                var promotion1 = quantity.toString();
                promotion1 = promotion1.substring(0, 2);
                promotion1 = parseInt(promotion1) * 5
                row.find(".jsPromotion").val(promotion1);
            }
            if (quantity >= 10000) {
                var promotion1 = quantity.toString();
                promotion1 = promotion1.substring(0, 3);
                promotion1 = parseInt(promotion1) * 5
                row.find(".jsPromotion").val(promotion1);
            }
            var promo = parseFloat(row.find(".jsPromotion").val());
            if (isNaN(promo)) promo = '';
            if (promo + quantity > iven) {
                if (confirm("Số lượng tồn không đủ! Bạn có muốn tiếp tục?")) {

                } else {
                    row.find(".jsPromotion").val("");
                    row.find(".jsQuantity").val("");
                    row.find(".jsTotal").val("");
                }
            }
        } else {
            if (quantity < 100) {
                promotion = promotion.toString();
                promotion = "";
                row.find(".jsPromotion").val(promotion);
            }
            if (quantity >= 100) {
                var promotion1 = quantity.toString();
                promotion1 = promotion1.substring(0, 1);
                row.find(".jsPromotion").val(promotion1);
            }
            if (quantity >= 1000) {
                var promotion1 = quantity.toString();
                promotion1 = promotion1.substring(0, 2);
                row.find(".jsPromotion").val(promotion1);
            }
            if (quantity >= 10000) {
                var promotion1 = quantity.toString();
                promotion1 = promotion1.substring(0, 3);
                row.find(".jsPromotion").val(promotion1);
            }
            var promo = parseFloat(row.find(".jsPromotion").val());
            if (isNaN(promo)) promo = '';
            if (promo + quantity > iven) {
                if (confirm("Số lượng tồn không đủ! Bạn có muốn tiếp tục?")) {

                } else {
                    row.find(".jsPromotion").val("");
                    row.find(".jsQuantity").val("");
                    row.find(".jsTotal").val("");
                }
            }
        }
    } else if (cusno != 'V00079') {
        if (quantity < 100) {
            promotion = promotion.toString();
            promotion = "";
            row.find(".jsPromotion").val(promotion);
        }
        if (quantity >= 100) {
            var promotion1 = quantity.toString();
            promotion1 = promotion1.substring(0, 1);
            row.find(".jsPromotion").val(promotion1);
        }
        if (quantity >= 1000) {
            var promotion1 = quantity.toString();
            promotion1 = promotion1.substring(0, 2);
            row.find(".jsPromotion").val(promotion1);
        }
        if (quantity >= 10000) {
            var promotion1 = quantity.toString();
            promotion1 = promotion1.substring(0, 3);
            row.find(".jsPromotion").val(promotion1);
        }
        var promo = parseFloat(row.find(".jsPromotion").val());
        if (isNaN(promo)) promo = '';
        if (promo + quantity > iven) {
            if (confirm("Số lượng tồn không đủ! Bạn có muốn tiếp tục?")) {

            } else {
                row.find(".jsPromotion").val("");
                row.find(".jsQuantity").val("");
                row.find(".jsTotal").val("");
            }
        }
    }
}