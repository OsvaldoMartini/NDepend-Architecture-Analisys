
CallGetAllSearched();

var filtered = [];
for (var i in dataSearched) {
    if (dataSearched.hasOwnProperty(i)) {

        var item = dataSearched[i];

        filtered.push({
            "id": item.Id,
            "name": item.PlaceName,
            "lat": item.GeoLat,
            "lng": item.GeoLong,
            "opening": item.OpeningHours
        });

    }
}


$(function () {

    $("#GridLocals").jqGrid
    ({
        datatype: 'local',
        data: filtered,
        //table header name
        colNames: ['id', 'Name', 'Opening', 'Latitude', 'Longitude'],
        //colModel takes the data from controller and binds to grid
        colModel: [
            {
                name: 'id',
                editable: true,
                key: true,
                hidden: true,
                search: false
            },
            {
                name: "name", editable: true, editrules: { required: true }, search: true
            },
            {
                name: "opening", editable: true, editrules: { required: true }, search: false

            },
            {
                name: "lat", editable: true, editrules: { required: true }, search: false,
                formatter: formatterColor
            },
            {
                name: "lng", editable: true, editrules: { required: true }, search: false,
                formatter: formatterColor
            }

        ],
        height: '100%',
        viewrecords: true,
        caption: 'W. Services',
        emptyrecords: 'No records',
        rowNum: 10,

        pager: jQuery('#pager'),
        rowList: [10, 20, 30, 40],

        jsonReader:
        {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true
    }).navGrid('#pager',
        {
            edit: true,
            add: true,
            del: true,
            search: true,
            refresh: true,
            closeAfterSearch: true
        }

    );

    $('#GridLocals').jqGrid('sortGrid', 'name', true, 'asc');


});


//formatter value Red Blue and Decimal Separator
function formatterColor(cellvalue) {
    if (!isEmpty(cellvalue) && (cellvalue < 0)) {
        return '<span class="currency" style="background-color: #66ff66; display: block; width: 100%; height: 100%; ">' +
            cellvalue +
            '</span>';
    } else if (!isEmpty(cellvalue) && (cellvalue > 3)) {
        return '<span class="currency"style="background-color: #FFFF99; display: block; width: 100%; height: 100%; ">' +
            cellvalue +
            '</span>';
    } else
        return cellvalue;

}

