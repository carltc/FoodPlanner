
// Move to the next instruction step and hide previous one.
function NextInstructionsStep() {

    var instructionsContainer = document.getElementById('InstructionsContainers');
    var instructionsContainers = instructionsContainer.getElementsByClassName('InstructionContainer');

    for (i = 0; i < instructionsContainers.length - 1; i++) {
        if (instructionsContainers[i].style.display != "none") {

            if (i < instructionsContainers.length - 1) {
                instructionsContainers[i].style.display = "none";
                instructionsContainers[i + 1].style.display = "";
                return;
            }
        }
    }

    if (instructionsContainers.length > 0) {
        // if nothing has been opened then open up the last one
        instructionsContainers[instructionsContainers.length - 1].style.display = "";
    }
}

// Move to the previous instruction step and hide the current one.
function PreviousInstructionsStep() {

    var instructionsContainer = document.getElementById('InstructionsContainers');
    var instructionsContainers = instructionsContainer.getElementsByClassName('InstructionContainer');

    for (i = instructionsContainers.length - 1; i >= 0; i--) {
        if (instructionsContainers[i].style.display != "none") {

            if (i > 0) {
                instructionsContainers[i].style.display = "none";
                instructionsContainers[i - 1].style.display = "";
                return;
            }
        }
    }

    if (instructionsContainers.length > 0) {
        // if nothing has been opened then open up the first one
        instructionsContainers[0].style.display = "";
    }
}

// Format the instruction text when it changes
function InstructionTextChanged(instructionElement) {

    var t = instructionElement.value;

    t = FormatInstructionText(t);

    console.log(t);

    instructionElement.value = t;
}

// Format the instruction text to proper syntax for ingredients
function FormatInstructionText(inputText) {

    var ingredientsRaw = document.getElementById('RecipeIngredients').value;
    var ingredients = eval('(' + ingredientsRaw + ')');

    var outputText = inputText;

    for (i = 0; i < ingredients.length; i++) {

        var ingredient = ingredients[i];
        var regex = new RegExp('[^"]' + ingredient.Name + '[^"]', "gi");
        outputText = outputText.replace(regex, ' "' + ingredient.Name + '" ');

    }

    return outputText;
}

// Search the text of the given element for any mention of recipe ingredients
function IngredientHighlight(element) {

    var ingredientsRaw = document.getElementById('RecipeIngredients').value;
    var ingredients = eval('(' + ingredientsRaw + ')');

    for (i = 0; i < ingredients.length; i++) {

        var ingredient = ingredients[i];

        if (ingredient.Name == element.innerText) {
            console.log("Found ingredient: " + ingredient.Name);



            break;
        }

    }

}

// Open the highlight window for the given ingredient
function OpenIngredientHighlight(highlightName) {
    var highlightElement = document.getElementById(highlightName);
    highlightElement.style.opacity = 1;
    highlightElement.style.visibility = 'visible';
}

// Close the highlight window for the given ingredient
function CloseIngredientHighlight(highlightName) {
    var highlightElement = document.getElementById(highlightName);
    highlightElement.style.opacity = 0;
    highlightElement.style.visibility = 'hidden';
}