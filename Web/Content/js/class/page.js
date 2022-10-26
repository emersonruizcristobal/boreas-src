class Page {

    constructor(baseUrl){
        this.baseUrl = baseUrl;
    }

    validate(form, entityName){
        var entity      = {};
        var isValid     = true;
        var messages    = [];

        $(form).find('input, textarea').each(function () {

            var type        = $(this).attr('type');
            var id          = $(this).attr('id')        || '';
            var name        = $(this).attr('name')      || '';
            var required    = $(this).data('required') || '';

            if (type == 'text' || type == 'hidden' || type == 'number' || type == 'password' || type == 'email') {
                if (name != '') {
                    if ($(this).val() == '' && required) {
                        isValid = false;
                        messages.push(name);
                    }
                }

                entity[entityName + '.' + name] = $(this).val();
            }
        });

        return {
            'Valid'     : isValid,
            'Entity'    : entity,
            'Message'   : messages
        }
    }

    validateWithChild(form, entityName, children, childrenName){


        var v = this.validate(form, entityName);
        var e = v.Entity;

        if(children.length == 0){
            v.Valid     = false;
            v.Message.push(childrenName);
        }else{
            e[entityName + '.' + childrenName] = children; 
        }

        return {
            'Valid' : v.Valid,
            'Entity' : e,
            'Message' : v.Message
        }

    }

    save(data, method, element, successMessage, failedMessage, callback){

        element.attr('disabled', true);

        $.ajax({
            type: "POST",
            url: this.baseUrl + method,
            data: data,
            context: document.body,
            dataType: 'json',
        }).done(function (data) {
            notify(successMessage, 'success');
            element.attr('disabled', false);
            callback();
        }).fail(function (data) {
            notify(failedMessage, 'error');
            element.attr('disabled', false);
        });

    }

    saveDirect(data, method, callback){
        $.ajax({
            type: "POST",
            url: this.baseUrl + method,
            data: data,
            context: document.body,
            dataType: 'json',
        }).done(function (data) {
            callback();
        }).fail(function (data) {
        });
    }

    get(method){

        var url = this.baseUrl;

        return new Promise(function(resolve, reject) {

            var isError = false;
            var data = [];

            $.ajax({
                type: "GET",
                url: url + method,
                context: document.body,
                dataType: 'json',
                async: false,
            }).done(function (data) {
                resolve(data);
            }).fail(function (data) {
                //error
            });
        });

    }

    upload(method, form){

        return new Promise(function(resolve, reject) {
            $.ajax({
                url:  this.baseUrl + method,
                type: 'POST',
                data: new FormData($(form)[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (file) {
                    $("#upload-progress").hide();
                    resolve(file)
                },
                error: function (b) {
                    console.log(b);
                },
                xhr: function () {
                    var fileXhr = $.ajaxSettings.xhr();
                    if (fileXhr.upload) {
                        $("#upload-progress").show();
                        fileXhr.upload.addEventListener("progress", function (e) {
                            if (e.lengthComputable) {
                                $("#upload-progress").attr({
                                    value   : e.loaded,
                                    max     : e.total
                                });
                            }
                        }, false);
                    }
                    return fileXhr;
                }
            });
        })

    }

    getAll(keywords){

    }

    delete(id){

    }

    update(data){

    }

    displayTable(method){

        var url = this.baseUrl;

        return new Promise(function(resolve, reject) {

            var isError = false;
            var data = [];

            $.ajax({
                type: "GET",
                url: url + method,
                context: document.body,
                dataType: 'json',
                async: false,
            }).done(function (data) {
                resolve(data);
            }).fail(function (data) {
                //error
            });
        });





    }
}

function hideField(propertyName){

    if(propertyName  != 'Id' && 
                        propertyName != 'Order' && 
                        propertyName != 'Tag' && 
                        propertyName != 'CreatedAt' && 
                        propertyName != 'UpdatedAt' &&
                        propertyName != 'Images' &&
                        !propertyName.includes("Id")){
        return true;
    }else{
        return false;
    }

}