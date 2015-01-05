Clearing-Windows-AppData
========================

In this Windows Store app sample I show you how to:

 - Create a custom flyout to view and manage all application data containers. This includes local, roaming, temporary, local cache, local settings and roaming settings.
 - Add the custom flyout to the SettingsPane of your app at runtime.
 - Restrict the display of this settings pane to only show up when your app runs in DEBUG mode.
 - Add test data to all application data containers.
 - Get a link to the location of each application data container.
 - Clear any of the application data containers.

To hear why I created this sample, read the Backstory section at the bottom of this page. 
The following image shows the app in action, with the custom flyout expanded on the right-hand side.
![custom flyout] (https://raw.githubusercontent.com/AndrewJByrne/assets/master/i/clearing.windows.appdata/app.screenshot.PNG)

As shown in the preceding screenshot, you can use the main page of the application to add some test data to the various application data containers, and use the flyout to clear these containers. The main page UI in the sample is deliberately basic. However, you should be able to use the flyout as-is in your app to help you get to a clean state as you work with the application data container(s) of your choice. 
Key Windows 8.1 API used in this sample
-------

To build the app
----
Just build and run the sample in Visual Studio. Once running use the main page UI to add some dummy data to a specific store. Then swipe from the right edge or tap the settings button to bring up the DebugSettings flyout. From there, you can clear data containers. 

You'll also notice little *copy to clipboard* button beside each data container in the settings flyout. Tapping this will load the path to that container into the clipboard. You can then paste that into Windows Explorer to open the specific folder. 

Useful resources
-------

Background story
-------
Most non-trivial will apps will, at some point, need to store app state or user settings. Windows 8.1 gives you alot of flexibility in how, and where, you store this data. As an heroic arachnid was once told, with great flexibility (or power) comes great responsibility. Reading docs just isn't enough to make sure the data I save for my app will be available when and where I need it. 

In an app I was recently building I wanted to harness the power of roaming data to roam app settings and app data between all machines on which this app was installed by the user. The API itself is straightforward, but I still like to test and see for myself what the data looks like on disk and what happens under various scenarios. What happens when I load roaming settings or a roaming file from disk and they are empty? What happens when I when I reach a limit on how much data I can actually roam (yes, there's a limit). 

As I proceeded to test my app, I realized there was no easy way to clear the roamig settings and folder beyond uninstalling the app. While that is possible durign the development of my app, it is a few extra clicks and prone to human error. Then, I discovered some useful APIs that would clear the roaming data for me. I then realized I really would only want to call these APIs in my case while I was debugging the app. 

And so, this sample was conceived. It is proving very useful to have an easy way from within my app to clear out roaming data whenever I feel the need. No uninstalling the app unless I really need to. Of course, I became more curious and decided to play with celaring out other application data containers - local, local cache, local settings and the temporary folder. I added these explorations to this sample too and now I have a handy flyout that is hooked up to clear this data too. 

Copyright
=======

Copyright (c) Andrew J Byrne. All rights reserved.

----------
