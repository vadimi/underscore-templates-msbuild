;
var compile = function (templateString) {
    return _.template(templateString).source;
};

var setTemplateSettings = function (settings) {
    if (!settings)
        return;
    
    if (settings.interpolate && settings.interpolate.length > 0)
        _.templateSettings.interpolate = new RegExp(settings.interpolate, 'g');
    
    if (settings.evaluate && settings.evaluate.length > 0)
        _.templateSettings.evaluate = new RegExp(settings.evaluate, 'g');

    if (settings.escape && settings.escape.length > 0)
        _.templateSettings.escape = new RegExp(settings.escape, 'g');
};