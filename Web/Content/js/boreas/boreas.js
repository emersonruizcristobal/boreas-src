(function ($) {

    $.fn.boreas = function (options, callback) {

        var currentTemp = 0;
        var settings = $.extend({
            minTemp:    -30,
            maxTemp:    200,
            celsius:    null,
            fahrenheit:  null,
            step:       5
        }, options);

        

        var toFahrenheit = function (c) {
            return (c * (9 / 5)) + 32;
        }

        var toCelsius = function (f) {
            return (f - 32) * (5 / 9);
        }

        var wrap = function (html, cl) {
            return '<div class="' + cl + '">' + html + '</div>';
        }

        var format = function (n) {
            var r = parseFloat(n).toFixed(0);
            return r;
        }

        var checkValues = function () {
            if (settings.celsius == null && settings.fahrenheit == null) {
                return settings.maxTemp;
            } else {
                currentTemp = settings.celsius;
                return settings.celsius;
            }
        }


        var ui = function () {

            var celsius     = '';
            var main        = '<div class="dv-temp-control"></div>';
            var fahrenheit  = '';

            for (var i = settings.maxTemp; i >= settings.minTemp; i -= settings.step) {
                celsius     += '<div class="dv-box right">' + format(i) + '</div>';
                fahrenheit  += '<div class="dv-box left">' + format(toFahrenheit(i)) + '</div>';
            }
            return wrap('<label>Celsius:</label> <input class="fld-celsius" type="text" value="' + format(checkValues()) + '" /><br />' + '<label>Fahrenheit:</label> <input class="fld-farenheit" type="text" value="' + format(toFahrenheit(checkValues())) + '" />', 'dv-input') +
                wrap(celsius, 'dv-celsius') +
                wrap(main, 'dv-main') +
                wrap(fahrenheit, 'dv-farenheit') +
                '<br class="clear" />';

        };

        this.html(ui());

        var input   = this.find('.dv-input');
        var control = this.find('.dv-temp-control');
        var cel     = input.find('.fld-celsius');
        var far     = input.find('.fld-farenheit');
        currentTemp = settings.maxTemp;

        var startPosition = 0;

        

        var onStartDrag = function () {
            let position    = control.position();
            let top         = position.top;

            startPosition   = top;
        }
        var onStopDrag = function () {
            let position    = control.position();
            let top         = position.top;

            var diff        = top - startPosition;
            var sub         = settings.step * (diff / 15);
            currentTemp     = currentTemp - sub;

            updateValues();
        }

        var checkFloorAndCeiling = function (uom, val) {
            if (uom == 'f') {
                return !(parseFloat(toCelsius(val)) > parseFloat(settings.maxTemp) || parseFloat(toCelsius(val)) < parseFloat(settings.minTemp));
            } else {
                return !(parseFloat(val) > parseFloat(settings.maxTemp) || parseFloat(val) < parseFloat(settings.minTemp));
            }
        }

        var moveTempControl = function (uom, c1, c2) {
            var temp = c1.val();
            if (!isNaN(temp)) {
                c1.val(parseFloat(temp).toFixed(2));
                if (checkFloorAndCeiling(uom, temp)) {
                    temp            = (uom == 'c') ? temp : toCelsius(temp);
                    var converted   = (uom == 'c') ? toFahrenheit(temp) : toCelsius(temp);
                    var top         = ((settings.maxTemp - temp) / settings.step) * 15;
                    control.css({ top: top + 'px' });
                    c2.val(parseFloat(converted).toFixed(2));
                    currentTemp     = temp;
                } else {
                    alert(temp + ' is not within the floor/ceiling of the allowed values');
                    c1.val(parseFloat(currentTemp).toFixed(2));
                }
            } else {
                alert(temp + ' is not a number');
                c1.val(parseFloat(currentTemp).toFixed(2));
            }
        }

        control.draggable({
            start:          onStartDrag,
            containment:    this.find('.dv-main'),
            stop:           onStopDrag
        });

       

        cel.on('blur', function () {
            moveTempControl('c', cel, far);
        });

        far.on('blur', function () {
            moveTempControl('f', far, cel);
        });

        this.find('.dv-main').css('height', this.find('.dv-celsius').height() + 'px');
        var id = (this[0] != null) ? this[0].id : '';

        moveTempControl('c', cel, far);
        var updateValues = function () {
            var v = {
                'celsius'   : parseFloat(currentTemp).toFixed(2),
                'fahrenheit': parseFloat(toFahrenheit(currentTemp)).toFixed(2)
            }

            cel.val(format(v.celsius));
            far.val(format(v.fahrenheit));

            if (callback != null) {
                return callback({
                    celsius:    v.celsius,
                    fahrenheit: v.fahrenheit,
                    step:       settings.step,
                    id:         id,
                    floor:      settings.minTemp,
                    ceiling:    settings.maxTemp
                });
            }
        }

    };

}(jQuery));