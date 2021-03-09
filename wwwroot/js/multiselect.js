$('#btnLaborRight').click(function (e) {
    var selectedLabors = $('#selectedLabors option:selected');
    if (selectedLabors.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#availLabors').append($(selectedLabors).clone());
    $(selectedLabors).remove();
    e.preventDefault();
});

$('#btnLaborLeft').click(function (e) {
    var selectedLabors = $('#availLabors option:selected');
    if (selectedLabors.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#selectedLabors').append($(selectedLabors).clone());
    $(selectedLabors).remove();
    e.preventDefault();

});

$('#btnMaterialRight').click(function (e) {
    var selectedMaterials = $('#selectedMaterials option:selected');
    if (selectedMaterials.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#availMaterials').append($(selectedMaterials).clone());
    $(selectedMaterials).remove();
    e.preventDefault();
});

$('#btnMaterialLeft').click(function (e) {
    var selectedMaterials = $('#availMaterials option:selected');
    if (selectedMaterials.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#selectedMaterials').append($(selectedMaterials).clone());
    $(selectedMaterials).remove();
    e.preventDefault();

});

$('#btnProjectSubmit').click(function (e) {
    $('#selectedLabors option').prop('selected', true);
    $('#selectedMaterials option').prop('selected', true);
});

//ProductionPlan Listboxes
$('#btnProdLaborRight').click(function (e) {
    var selectedProdLabors = $('#selectedProdLabors option:selected');
    if (selectedProdLabors.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#availProdLabors').append($(selectedProdLabors).clone());
    $(selectedProdLabors).remove();
    e.preventDefault();
});

$('#btnProdLaborLeft').click(function (e) {
    var selectedProdLabors = $('#availProdLabors option:selected');
    if (selectedProdLabors.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#selectedProdLabors').append($(selectedProdLabors).clone());
    $(selectedProdLabors).remove();
    e.preventDefault();

});

$('#btnProdMaterialRight').click(function (e) {
    var selectedProdMaterials = $('#selectedProdMaterials option:selected');
    if (selectedProdMaterials.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#availProdMaterials').append($(selectedProdMaterials).clone());
    $(selectedProdMaterials).remove();
    e.preventDefault();
});

$('#btnProdMaterialLeft').click(function (e) {
    var selectedProdMaterials = $('#availProdMaterials option:selected');
    if (selectedProdMaterials.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }
    $('#selectedProdMaterials').append($(selectedProdMaterials).clone());
    $(selectedProdMaterials).remove();
    e.preventDefault();

});

$('#btnProductionPlanSubmit').click(function (e) {
    $('#selectedProdLabors option').prop('selected', true);
    $('#selectedProdMaterials option').prop('selected', true);
});

$('#btnEmpMoveRight').click(function (e) {
    var selectedOpts = $('#selectedOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#availOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

$('#btnEmpMoveLeft').click(function (e) {
    var selectedOpts = $('#availOptions option:selected');
    if (selectedOpts.length == 0) {
        alert("Nothing to move.");
        e.preventDefault();
    }

    $('#selectedOptions').append($(selectedOpts).clone());
    $(selectedOpts).remove();
    e.preventDefault();
});

$('#btnSave').click(function (e) {
    $('#selectedOptions option').prop('selected', true);
});


