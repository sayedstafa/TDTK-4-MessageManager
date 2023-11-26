# Message Manager for TDTK 4 Documentation

## Overview

The Message Manager is a custom script designed to integrate with Tower Defense Toolkit 4 (TDTK 4) in Unity. It provides a flexible way to display messages and play audio clips at specific points during a level, such as the start of the level, the beginning of a wave, or after a certain delay during a wave. This adds an extra layer of interactivity and engagement, enhancing the player's experience.

## Installation

### Step 1: Setting Up the Necessary GameObjects
1. Create a UI Canvas: If you don’t already have a UI Canvas in your scene, create one. This will be used to display the messages.
2. Add a TextMeshPro - Text (UI) component within the Canvas. This is where your messages will be displayed.
3. Create an Empty GameObject in your scene and name it `MessageManager`. This will hold the Message Manager script.
4. Add an AudioSource to the `MessageManager` GameObject if you plan to play audio clips with your messages.

### Step 2: Preparing the Message Manager Script
1. Add the `MessageManager` script to the `MessageManager` GameObject.
2. Assign the TextMeshPro component to the script. This links the UI text element to the script for displaying messages.
3. Add an AudioSource component (if using audio) to the script. This will be used to play audio clips associated with messages.

### Step 3: Integrating with SpawnManager
1. Open the `SpawnManager.cs` script from TDTK 4.
2. Locate the `SpawnNextWave` method within the script.
3. Add a method call to `MessageManager.DisplayMessageForWave(currentWaveIdx)` at the beginning of the `SpawnNextWave` method. This ensures that the message corresponding to the current wave is displayed when the wave starts.

   SpawnManager.cs:
   ```
		private void SpawnNextWave(){	//actual function to spawn next wave
			if(!GameControl.HasGameStarted()) GameControl.StartGame();
			
			if(spawning) return;
			
			readyToSpawn=false;
			SetTimeToNextWave(-1);
			currentWaveIdx+=1;

            // Trigger MessageManager here
            if (MessageManager.instance != null)
            {
                MessageManager.instance.DisplayMessageForWave(currentWaveIdx);
            }

            if (!IsEndlessMode()) StartCoroutine(SpawnWave(waveList[currentWaveIdx]));
			else{
				waveList.Add(generator.Generate(currentWaveIdx+2));
				int waveIdx=GetListIndexFromWaveIndex(currentWaveIdx);
				StartCoroutine(SpawnWave(waveList[waveIdx]));
			}
			
			TDTK.OnNewWave(currentWaveIdx+1);
			AudioManager.OnNewWave();
		}
   ```

## How to Use the Message Manager

1. Adding Messages: In the Unity Editor, select the `MessageManager` GameObject. In the Inspector, you will see fields to add messages. Each message requires you to specify:
   - The wave number when the message should appear.
   - The message text.
   - The delay before the message is displayed (in seconds).
   - The duration for how long the message stays on screen.
   - An optional audio clip to play along with the message.

2. Configuring Messages: Use the serialized fields in the Inspector to configure your messages. You can add multiple messages for different waves, each with its own settings.

3. Testing Messages: Run your scene to test the messages. Messages will appear at the specified times based on the game’s wave progression.

4. Adjusting Messages: You can return to the Inspector at any time to adjust the messages' settings, add new messages, or remove existing ones.

5. Audio Clips: If you have added an audio clip to a message, ensure your scene’s Audio Listener is set up correctly to hear the playback.

This Message Manager script provides a simple yet powerful way to enhance the player's experience in your TDTK 4 game by adding timed messages and audio cues. Adjust and extend the script as needed to fit the specific needs of your game.