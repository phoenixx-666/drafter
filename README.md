# drafter
A simple app to fill drafts for the [Heroes of the Storm Liquipedia](https://liquipedia.net/heroes).

## Manual
This application allows you to enter hero names by only typing the first letters, with the rest being done by the autocomplete:

![Main window screenshot](/Screenshot.png?raw=true "Main window screenshot")

You can navigate between the text boxes using `Tab`, `Shift+Tab` (backwards) or `Ctrl+Tab` (left team to right team and vice-versa)

The bottom frame, "Tab Order", allows you to change the Tab Order of the text boxes:
* *Bans Then Picks*, if you want to fill the form using the screenshot when all the heroes and bans are already selected.
* *First Pick Left* or *First Pick Right*, if you want to fill the draft live, depending on which team was given the First Pick.

The checkboxes left and right to the Battleground field define which team is the winner of a match.

Then you can Swap the Teams (`Ctrl+W`) if the tournament bracket lists them in the reverse order.

After you finished with filling the match data, you should press "*Copy*" (hotkeys: `Ctrl+Shift+C`, `Ctrl+S`) to copy the resulting wiki code into the clipboard.

The hotkey to clear the form is `Ctrl+Space`.

### Editing
You can edit the match data from Liquipedia by parsing the wiki code from the clipboard. Use "*Paste Clipboard*" (hotkeys: `Ctrl+Shift+V`, `Ctrl+R`).

### Converting a draft screenshot to wiki code
The application is also able to analyze draft screenshots and convert them to wiki code. You will need the screenshot of full draft after the last hero is picked (for example, the swap round). The best result are yielded by the screenshots with `1920x1080` resolution or higher, so better switch the stream in your web browser to fullscreen mode when `PrintScreen` the screenshot. Then use the same "*Paste Clipboard*" function (hotkeys: `Ctrl+Shift+V`, `Ctrl+R`). If the clipboard contains image, another window will open, namely the *Screenshot Viewer*, showing the results of recognition. If the result of the recognition is correct, then you will see an image like this:

![Screenshot viewer screenshot](/ScreenshotViewer.png?raw=true "Screenshot viewer screenshot")

If some search result (denoted by a colored rectangle) is incorrect or out of place, you will be able to delete it by selecting it (`Left Click`) and pressing `Delete`, and add the correct one by `Right Clicking` at any location within the hero portait and entering the hero name in the dialog window (leave the hero name empty if you are dealing with a wasted ban). The wiki code in the main window will be generated automatically.
