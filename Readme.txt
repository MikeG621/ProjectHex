# ProjectHex #

Author: [Michael Gaisser](mailto:mjgaisser@gmail.com)
Version: 0.0.5
Date: 150705

A project-based hex editor, with full tree navigation of binary files based on
project files created by the user. The projects define the file structure and
"building blocks" of the file, so defining a node once allows the use of the
node throughout the entire binary.

_______________________
### Version History ###

0.0.5, 150705
* [ADD] ProjectFile updates, namely Save(), Get/SetBytes(), min/max values and IsChild implementation
* [ADD] StringVar Encoding implementation
* [ADD] BinaryFile Save, SelectionDialog
* [ADD] Compute "**" exponential notation, "<>" and "~=" inequality notation
* [ADD] Equation "**" exponential notation
* [UPD] Equation will correctly return ''false'' for "||" and "&&"
* Bug fixes

0.0.4, 130910
* [ADD] Started laying out additional project-level defaults for StringVar and BoolVar
* [ADD] Operators for most VarTypes
* [UPD] Reading a Binary now implements a DeepCopy() method when loading Types
* [UPD] License changed to MPL 2.0

0.0.3, 130701
* [ADD] Additional commenting
* [ADD] Serializable
* [UPD] Modified some internal properties

0.0.2, 130613
* Text updates

0.0.1, 130421
* Initial Upload

### Installation Notes ###
Requires the use of [MikeG621\Common].

_______________________________
#### Copyright Information ####
Copyright © Michael Gaisser, 2012-

This software is subject to the terms of the Mozilla Public
License, v. 2.0. If a copy of the MPL (License.txt) was not distributed
with this file, You can obtain one at http://mozilla.org/MPL/2.0/