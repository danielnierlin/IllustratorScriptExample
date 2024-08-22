var defaultNumberDecimalSeperator = ",";
var newSeparator = ".";
var layerName = "MD_2_BEMASSUNG_NEW";

var doc = app.activeDocument;
var myLayer = doc.layers.getByName(layerName);

replaceDecimalSeparator(myLayer, newSeparator)

function replaceDecimalSeparator(layer, newSeparator) {
    var textFrames = getAllTextFrames(layer);

    for (var i = 0; i < textFrames.length; i++) {
        var myTextFrame = textFrames[i];
        var myContent = myTextFrame.contents;
        var index;

        // Alle Dezimaltrenner im Textrahmen ersetzen
        do {
            index = myContent.indexOf(defaultNumberDecimalSeperator);
            if (index === -1) {
                break;
            }

            myTextFrame.characters[index].contents = newSeparator;
            myContent = myTextFrame.contents; // Inhalt nach der �nderung neu laden
        } while (index !== -1);
    }
}

// Rekursive Funktion zum Ermitteln aller TextFrames in der Ebene
function getAllTextFrames(myLayer) {
    var myTextFrames = [];
    getTextFrames(myLayer.textFrames, myTextFrames);
    getTextFramesInGroups(myLayer.groupItems, myTextFrames);
    return myTextFrames;
}

function getTextFrames(frames, myTextFrames) {
    for (var i = 0; i < frames.length; i++) {
        myTextFrames.push(frames[i]);
    }
}

function getTextFramesInGroups(groups, mytextFrames) {
    var myGroup;

    for (var i = 0; i < groups.length; i++) {
        myGroup = groups[i];

        getTextFrames(myGroup.textFrames, mytextFrames);
        getTextFramesInGroups(myGroup.groupItems, mytextFrames);
    }
}
