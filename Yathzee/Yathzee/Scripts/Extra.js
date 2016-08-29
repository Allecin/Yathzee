
var keepDices = [true, true, true, true, true];
var rolesLeft = 3;

var imageIfLocked;
var imageIfUnlocked;

var lockedDice1 = new Image();
lockedDice1.src = '../Content/dices/dice1Locked.png';
var lockedDice2 = new Image();
lockedDice2.src = '../Content/dices/dice2Locked.png';
var lockedDice3 = new Image();
lockedDice3.src = '../Content/dices/dice3Locked.png';
var lockedDice4 = new Image();
lockedDice4.src = '../Content/dices/dice4Locked.png';
var lockedDice5 = new Image();
lockedDice5.src = '../Content/dices/dice5Locked.png';
var lockedDice6 = new Image();
lockedDice6.src = '../Content/dices/dice6Locked.png';
var notLockedDice1 = new Image();
notLockedDice1.src = '../Content/dices/dice1.png';
var notLockedDice2 = new Image();
notLockedDice2.src = '../Content/dices/dice2.png';
var notLockedDice3 = new Image();
notLockedDice3.src = '../Content/dices/dice3.png';
var notLockedDice4 = new Image();
notLockedDice4.src = '../Content/dices/dice4.png';
var notLockedDice5 = new Image();
notLockedDice5.src = '../Content/dices/dice5.png';
var notLockedDice6 = new Image();
notLockedDice6.src = '../Content/dices/dice6.png';

function reset() {
    rolesLeft = 3;
}

function keep(diceNumber, diceValue)
{

    var idImageToChange = "pd" + diceNumber + "vd" + diceValue;
    var imageToChange = document.getElementById(idImageToChange);

    //alert("Dicenumber: " + diceNumber + " dicevalue: " + diceValue + " idImageTochange: " + idImageToChange);
    //alert(imageToChange);

    SetImages(diceValue);

    //alert("imageifLocked " + imageIfLocked.src + " imageifnotlocked: " + imageIfUnlocked.src);
    
    if (keepDices[diceNumber])
    {
        keepDices[diceNumber] = false;
    }
    else
    {
        keepDices[diceNumber] = true;
    }

    //switch (diceNumber) {
    //    case 0:
    //        if (keepDices[diceNumber]) {
    //            document.getElementById("ice0").value = "Lock dice";
    //        } else {
    //            document.getElementById("ice0").value = "Unlock dice";
    //        };
    //        break;
    //    case 1: if (keepDices[diceNumber]) {
    //        document.getElementById("ice1").value = "Lock dice";
    //    } else {
    //        document.getElementById("ice1").value = "Unlock dice";
    //    };
    //        break;
    //    case 2: if (keepDices[diceNumber]) {
    //        document.getElementById("ice2").value = "Lock dice";
    //    } else {
    //        document.getElementById("ice2").value = "Unlock dice";
    //    };
    //        break;
    //    case 3: if (keepDices[diceNumber]) {
    //        document.getElementById("ice3").value = "Lock dice";
    //    } else {
    //        document.getElementById("ice3").value = "Unlock dice";
    //    };
    //        break;
    //    case 4: if (keepDices[diceNumber]) {
    //        document.getElementById("ice4").value = "Lock dice";
    //    } else {
    //        document.getElementById("ice4").value = "Unlock dice";
    //    };
    //        break;
    //}

    if (keepDices[diceNumber]) {
        imageToChange.src = imageIfUnlocked.src;
    } else {
        imageToChange.src = imageIfLocked.src;
    };
}

function SetImages(diceValue)
{
    switch (diceValue)
    {
        case 1: imageIfLocked = lockedDice1;
            imageIfUnlocked = notLockedDice1;
            break;
        case 2: imageIfLocked = lockedDice2;
            imageIfUnlocked = notLockedDice2;
            break;
        case 3: imageIfLocked = lockedDice3;
            imageIfUnlocked = notLockedDice3;
            break;
        case 4: imageIfLocked = lockedDice4;
            imageIfUnlocked = notLockedDice4;
            break;
        case 5: imageIfLocked = lockedDice5;
            imageIfUnlocked = notLockedDice5;
            break;
        case 6: imageIfLocked = lockedDice6;
            imageIfUnlocked = notLockedDice6;
            break;
    }
}
function roll(dice1Value, dice2Value, dice3Value, dice4Value, dice5Value)
{
    //alert(dice1Value + " "+ dice2Value +" " + dice3Value +" "+ dice4Value+" "+ dice5Value)
    var dice0 = document.getElementById("dice0");
    var dice1 = document.getElementById("dice1");
    var dice2 = document.getElementById("dice2");
    var dice3 = document.getElementById("dice3");
    var dice4 = document.getElementById("dice4");
    var dices = [dice0, dice1, dice2, dice3, dice4];
    var diceValues = [dice0.textContent, dice1.textContent, dice2.textContent, dice3.textContent, dice4.textContent];
    var diceXValues = [dice1Value, dice2Value, dice3Value, dice4Value, dice5Value];
    for (i = 0; i < keepDices.length; i++)
    {
        //alert("euhm: " + 1);
       if (keepDices[i])
       {
           //alert("hey");
           var value = throwDice();
           dices[i].textContent = value + "";
           diceValues[i] = value;

           var idImageToChange = "pd" + i + "vd" + diceXValues[i];
           alert(idImageToChange)
           var imageToChange = document.getElementById(idImageToChange);
           SetImages(value);
           imageToChange.src = imageIfUnlocked.src;
        }
    }
    rolesLeft = rolesLeft - 1;
    document.getElementById("rollsLeft").textContent = "Rolls left " + rolesLeft;
    updateOptions(diceValues);
    keepDices = [true, true, true, true, true];

    if (rolesLeft <= 0)
    {
        disableThrow();
    }

    //document.getElementById("ice0").value = "Lock dice";
    //document.getElementById("ice1").value = "Lock dice";
    //document.getElementById("ice2").value = "Lock dice";
    //document.getElementById("ice3").value = "Lock dice";
    //document.getElementById("ice4").value = "Lock dice";
}

function disableThrow()
{
    document.getElementById("roll").disabled = true;
}

function throwDice()
{
    var neww = Math.floor(Math.random() * 6) + 1
    return neww;
}

function updateOptions(diceValues)
{
    //alert("all dices: " + diceValues[0] + " " + diceValues[1] + " " + diceValues[2] + " " + diceValues[3] + " " + diceValues[4] + " ")
    $.ajax({
        url: "/Game/_UpdatePossibleScores",
        type: "POST",
        data: JSON.stringify(diceValues),
        contentType: "application/json",
        success: function (result) {
            //document.getElementById("posSco").innerHTML = result + "";
            //alert("Yes");
            $('#posSco').html(result);
        },
        error: function (error, text) {
            alert("Error: Coudn't reload possible scores. Error details: " + text);
        }
    });
}

function editProfile()
{
    var btnEdit = document.getElementById("edit");
    var privateChecked = true;
    var newName = "";

    if (btnEdit.value == "Edit profile")
    {
        document.getElementById("editDiv").style.visibility = "visible";
        btnEdit.value = "Save changes";

    }
    else {
        document.getElementById("editDiv").style.visibility = "hidden";
        btnEdit.value = "Edit profile";

        if (document.getElementById("rdbPrivate").checked) {
            privateChecked = true;
        } else {
            privateChecked = false;
        }
        newName = document.getElementById("newName").value;

        $.ajax({
            url: "/Home/UpdateProfile",
            type: "POST",
            data: { name: newName, privacy: privateChecked },
            success: function (result) {
                document.getElementById("nameNew").innerHTML = result.split(",")[0];
                document.getElementById("newSettings").innerHTML = result.split(",")[1];
                document.getElementById("title").innerHTML = "Profile " + result.split(",")[0];
                document.getElementById("profileChangesSuccess").style.visibility = "visible";
            },
            error: function (error, text) {
                //alert("Error: Coudn't reload possible scores. Error details: " + text);
                document.getElementById("profileChangesFail").style.visibility = "visible";            
            }
        });
    }
}

function viewProfile()
{
    var emailAddress = document.getElementById("emailAddress").value;

    $.ajax({
        url: "/Home/CheckPrivacy",
        type: "POST",
        data: { email: emailAddress },
        success: function (result) {
            if (result == "True")
            {
                $.ajax({
                    url: "/Home/GoToProfile",
                    type: "POST",
                    data: { email: emailAddress },
                    success: function (result) {
                        window.location.href = "/Home/Profile?otherPlayerId=" + result;
                    },
                    error: function (error, text) {
                        alert("Error heeeeere");
                        document.getElementById("Error").style.visibility = "visible";
                    }
                });
            }
            else
            {
                document.getElementById("invalidEmailAddress").style.visibility = "visible";
            }
        },
        error: function (error, text) {
            alert("Error here");
            document.getElementById("Error").style.visibility = "visible";            
        }
    });
}

function lolebol(cutnummer)
{
    
    var _img = document.getElementsByClassName("4");
    alert(_img);
    _img.src = lockedDice5.src;
}
