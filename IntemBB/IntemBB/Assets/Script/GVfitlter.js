
document.onkeydown = function (e) {
    if (e.ctrlKey && (e.keyCode === 85)) {
        return false;
    }
}

//function Search_Gridview(strKey) {
//    var strData = strKey.value.toLowerCase().split(" ");
//    var tblData = document.getElementById("<%=gvShowCart.ClientID %>");
//    var tblPromotion = document.getElementById("<%=gvPromotion.ClientID %>");
//    var rowData;
//    var rowData1;
//    for (var i = 1; i < tblData.rows.length; i++) {
//        rowData = tblData.rows[i].innerHTML;
//        var styleDisplay = 'none';
//        for (var j = 0; j < strData.length; j++) {
//            if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
//                styleDisplay = '';
//            else {
//                styleDisplay = 'none';
//                break;
//            }
//        }
//        tblData.rows[i].style.display = styleDisplay;
//    }

//    for (var i = 1; i < tblPromotion.rows.length; i++) {
//        rowData1 = tblPromotion.rows[i].innerHTML;
//        var styleDisplay = 'none';
//        for (var j = 0; j < strData.length; j++) {
//            if (rowData1.toLowerCase().indexOf(strData[j]) >= 0)
//                styleDisplay = '';
//            else {
//                styleDisplay = 'none';
//                break;
//            }
//        }
//        tblPromotion.rows[i].style.display = styleDisplay;
//    }
//}

function Search_GvData(strKey) {
    var strData = strKey.value.toLowerCase().split(" ");
    var tblData = document.getElementById("<%=gvData.ClientID %>");
    var rowData;

    for (var i = 1; i < tblData.rows.length; i++) {
        rowData = tblData.rows[i].innerHTML;
        var styleDisplay = 'none';
        for (var j = 0; j < strData.length; j++) {
            if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                styleDisplay = '';
            else {
                styleDisplay = 'none';
                break;
            }
        }
        tblData.rows[i].style.display = styleDisplay;
    }
}

