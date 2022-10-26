
var _defaultStep = 10;
var thermometerCount = 0;

$(function () {
    getAll();


    $('.btn-add').on('click', function () {
        var v = validateAddThermometer();
        if (v.isValid) {
            thermometerCount++;
            var html = '<div id="t' + thermometerCount + '" class="dv-thermometer"></div>';
            $('.dv-wrapper').append(html);
            $('#t' + thermometerCount).boreas({
                step: v.step,
                minTemp: v.floor,
                maxTemp: v.ceiling
            }, function (d) {
                save(d.celsius, d.fahrenheit, d.id, d.step, d.floor, d.ceiling);
            });
        } else {
            alert(v.error.join('\n'));
        }
        
    });
});

var validateAddThermometer = function () {

    var floor   = $('#fld-floor').val();
    var ceiling = $('#fld-ceiling').val();
    var step    = $('#fld-step').val();
    var isValid = true;
    var errors = [];

    if (isNaN(floor)) {
        isValid = false;
        errors.push(floor + ' is not a number');
    }
    if (isNaN(ceiling)) {
        isValid = false;
        errors.push(ceiling + ' is not a number');
    }
    if (isNaN(step)) {
        isValid = false;
        errors.push(step + ' is not a number');
    }
    if (isValid) {
        if (parseFloat(floor) > parseFloat(ceiling) || parseFloat(floor) == parseFloat(ceiling)) {
            isValid = false;
            errors.push('Floor should be lower than the ceiling');
        } else {
            if (parseFloat(step) > (parseFloat(ceiling) - parseFloat(floor))) {
                isValid = false;
                errors.push('Step should be lower than ceiling minus floor');
            }
        }
    }

    return {
        isValid : isValid,
        step    : step,
        floor   : floor,
        ceiling : ceiling,
        error   : errors
    }
}


var save = function (c, f, name, s, floor, ceiling) {
    $.ajax({
        type: "POST",
        url: "/thermometer/default/save",
        data: {
            'Thermometer': {
                'Celsius'   : c,
                'Fahrenheit': f,
                'Step'      : s,
                'Name'      : name,
                'Floor'     : floor,
                'Ceiling'   : ceiling
            }
        },
        context: document.body,
        dataType: 'json',
    }).done(function (data) {
        console.log(data);
    }).fail(function (data) {
        console.log(data);
    });
}

var getAll = function () {
    $.ajax({
        type: "GET",
        url: "/thermometer/default/getall",
        context: document.body,
        dataType: 'json'
    }).done(function (data) {
        var html = '';
        thermometerCount = data.length;
        for (var i = 0; i < data.length; i++) {
            html += '<div id="' + data[i].Name + '" class="dv-thermometer"></div>';
        }
        $('.dv-wrapper').html(html);
        if (html != '') {
            applyPlugin(data);
        }
    }).fail(function (data) {

    });
}
var applyPlugin = function (data) {
    for (var i = 0; i < data.length; i++) {
        var name = data[i].Name;
        $('#' + name).boreas({
            step:       data[i].Step,
            celsius:    data[i].Celsius,
            fahrenheit: data[i].Fahrenheit
        }, function (d) {
            save(d.celsius, d.fahrenheit, d.id, d.step, d.floor, d.ceiling);
        });
    }
}


$('#t1').boreas({
    step: 10,
    minTemp: -30,
    maxTemp: 200
}, function (d) {
    //callback for saving the response object
});