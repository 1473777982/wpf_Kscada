   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   bool2VisibilityConverter  WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor  SelectedBackgroundColor  SelectedUnfocusedColor 	 ControlLightColor 
 ControlMediumColor  ControlDarkColor  ControlMouseOverColor  ControlPressedColor  
GlyphColor  GlyphMouseOver  BorderLightColor  BorderMediumColor  BorderDarkColor  PressedBorderLightColor  PressedBorderDarkColor  DisabledBorderLightColor  DisabledBorderDarkColor  DefaultBorderBrushDarkColor  HeaderTopColor " DatagridCurrentCellBorderColor  SliderTrackDarkColor  NavButtonFrameColor  MenuPopupBrush  �� EndPoint �� 
StartPoint � Color � Offset $  ProgressBarIndicatorAnimatedFill �� 
TargetType �� 
ResourceId �� TypeInTargetAssembly&    System.Windows.Controls.DataGrid �� Property �� Value	 �� 
TargetType'  !System.Windows.VisualStateManager
   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty8  2System.Windows.Media.Animation.EasingColorKeyFrame �� KeyTime �� Value R� KeyTime  System.Windows.Visibility   	Collapsed R� Value ��Name J� SnapsToDevicePixels �Name J� Opacity � Points �� Stretch �� Color ��Name �� Padding ��Name � ColumnDefinitions � RowDefinitions     SelectAllCommand7  1System.Windows.Controls.DataGridHeadersVisibility
!   All" �� ConverterParameter!#    HeadersVisibilityConverter$ �� 	Converter% �� RelativeSource& �� AncestorTypeG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter'  Name(   Column) ��Name* �� CanContentScroll+ ��Name, �� Orientation- �� ViewportHeight. �� ViewportSize/ �� ScrollableHeight0 � Maximum*1 �� #ComputedVerticalScrollBarVisibility2 � Value3 �� Mode4 �� ViewportWidth5 �� ScrollableWidth,6 �� %ComputedHorizontalScrollBarVisibility7 �� Triggers8 P� Property9 P� Value*  $System.Windows.Controls.DataGridCell)	  #System.Windows.Controls.DataGridRow?
  9System.Windows.Controls.Primitives.SelectiveScrollingGrid?  9System.Windows.Controls.Primitives.DataGridCellsPresenter: 	  
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter; 	  DetailsVisibility$< 
  SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation=   Vertical#>    RowDetailsScrollingConverter:  4System.Windows.Controls.Primitives.DataGridRowHeader
?   Row  RowHeaderGripperStyle@ �� OrientationA n�Name  ColumnHeaderGripperStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeaderB  NameC J� IsHitTestVisible�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      5      %:?  &	        &	      &	 4     &	 U     &	 v     &	 �     &	 �     &	 �     &	 �     &		     &	
 8    &	 Y    &	 z    &	 �    &	 �    &	 �    &	      &	 A    &	 b    &	 �    &	 �    &	 �    &	 �    &	     &	 $    &	 E    &	 b    &	 �    &	 �    &	 k    (�� �    5O      $" DataGridSelectAllButtonStyle��"   )7  7  '   �    '  �    7  '	  )    7  7  7 
 7  7 
 7  7 
 7  7 
 7  7 
 7  7 
 7  7 
 7  7  7  7  7  &	 8.    '  ^/    7  7   7  7  &	 	7    '  /8    7  7  '  1<    �� 5      6#   ��5      	#FFE8EDF9��6+   ��5      	#FFC5CBF9��65   ��5      	#FF7381F9��64   ��5
      	#FFE8EDF9��69   ��5      	#FFC5CBF9��68   ��5      	#FF888888��67   ��5      	#FFC5CBF9��67   ��5      	#FFDDDDDD��66   ��5      	White��6-   ��5      	#FF7381F9��62   ��5      	#FF211AA9��60   ��5      	#FF3843C4��65   ��5      	#FF211AA9��63   ��5      	#FF444444��6*   ��5      /+sc#1, 0.004391443, 0.002428215, 0.242281124��6P   ��5      	#FFCCCCCC��60   ��5      	#FF888888��61   ��5      	#FF444444��6/   ��5!      	#FF888888��67   ��5"      	#FF444444��66   ��5$      	#FFAAAAAA��68   ��5%      	#FF888888��67   ��5'      	Black��67   ��5*      	#FFC5CBF9��6.   ��5+      	Black��6:   ��5,      	#FFC5CBF9��64   ��5.      	#FF3843C4��63   �� 50      $  0.5,1&�51      $ 0.5,0&�52      .��53   
   � # � 	 6   $ 0Q�54      � 55   
   # � 
 6   $	 0.5Q�56      � 57   
   # � 	 6   $ 1Q�58      59      �� 5;      $	 0,0&�5<      $	  1,0&�5=      ��5?      � � 5@      $ 	#000000FF��6   $ 0Q�5A      � 5B      $ 	#600000FF��6   $	 0.4Q�5C      � 5D      $ 	#600000FF��6   $	 0.6Q�5E      � 5F      $ 	#000000FF��6   $ 1Q�5G      5H      5J      �� 5N      " ��6   .�5Q   
   ��  � �� 5R      �� 5S      "	 ��6"   .R�5T      � 
 5U        5V      CommonStates��. 5W   "     
Normal��6.     5X   "   	MouseOver��. 5Y   &   �� .��5Z   *   �� $ Border��6G   $_ Y(Shape.Fill).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�5[   1   .g�5]   .     $ 0��6B   8   5^   *   5_   +   5`   '   5a   #     5b   "   Pressed��. 5c   &   �� .��5d   *   �� $ Border��6G   $_ Y(Shape.Fill).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�5e   1   .g�5g   .     $ 0��6B   8  5h   *   5i   +   5j   '   5k   #     5l   "   Disabled��. 5m   &   �� .��5n   *   T� $ (UIElement.Visibility)�6H   $ Arrow��5o   2   ..�5p   .   i� $ 0��6E   # Z 5q   -   5r   +   5s   '   5t   #   5u      .+�5w      �� $ Border��6$    . ��5y      �� 5z   "   $  0.5,1&�66   $ 0.5,0&�5{   $   .��5|   &   � # �  63   $ 0Q�5}      � 5~   &   # �  63   $ 1Q�5      5�   #   ��5�      �� 5�   "   $  0.5,1&�66   $ 0.5,0&�5�   $   .��5�   &   � # � 	 63   $ 0Q�5�      � 5�   &   # � 
 63   $ 1Q�5�      5�   #   5�      � 5�      $ Arrow��6"   $��Right=�5�      $��8,8,3,3q�5�      $
 0.15Q�5�       �   ��� ��� ��� ��� $ Uniform=�5�      $��Bottom=�5�      ��5�      �� 5�   "   # �  62   5�      5�      5�      5�      5�      �� 5�      "   6   .�5�   
   ��  � ��
 �   �6'   �� 5�   
    � �� 5�      �� 5�      $  0.5,1&�6&   $ 0.5,0&�5�      .��5�      � # �  6#   $ 0Q�5�      � 5�      # �  6#   $ 1Q�5�      5�      5�      �� 5�   
    � ��$ 1q�5�      �� 5�   
     �@  RowDetailsVisibilityMode$ VisibleWhenSelected=�5�      �� 5�   
    � �� . 5�      �� 5�   
    �@��PanningMode$
 Both=�5�      �� 5�   
    �@��IsFlicksEnabled .  5�      �� 5�   
    � �� 5�      �� 5�      "	   6"   .R�5�      �� $ border��6    . #��z��6H   #��z��6t   # z��6�   ��5�      �� 5�      # � 	 6.   
 5�        5�      CommonStates��. 5�   "     Disabled��. 5�   &   �� .��5�   *   �� $ border��6G   $0 *(Panel.Background).(SolidColorBrush.Color)�6f   .g�5�   .     $ 0��6B   # � 	 6N   5�   +   5�   '   5�   #     5�   "   
Normal��6.   5�      ._�5�      �� $ DG_ScrollViewer��6'   |�.  
���   ���5�      �� 5�   "   "	 ��62   .R�5�   &   �  5�   *   �� 5�   .   $
��Auto �6?   �� 5�   .   $��* �6?   �� 5�   .   $
��Auto �6?    5�   *   �� 5�   .   $
��Auto �6<   �� 5�   .   $��* �6<   �� 5�   .   $
��Auto �6<   .+�5�   *   �� |�.  #��Z  6D   ��5�   1   C� *�� $" DataGridSelectAllButtonStyle��"   +y�5�   1   �� *HeadersVisibility+#" Z! #$ Z# % �� "&   ��5�   1   �� *CellsPanelHorizontalOffset+% �� "&     5�   *   $!' PART_ColumnHeadersPresenter��6I   $��1��6n   y�6~   �� *HeadersVisibility+#" Z( #$ Z# % �� "&   �� 5�   *   $!) PART_ScrollContentPresenter��6A   $��2��5�   +   $��1��5�   +   #* z��5�   +   �� 5�   *   $+ PART_VerticalScrollBar��64   $��2��6T   $��1��6e   $, Vertical=�6s   #. z- 5�   4   #0 z/ 6d   #y�z1 5�   4   2 5�   4   �� *VerticalOffset+$3 OneWay=�% �� *TemplatedParent+� 5�   *   $��1��6/   $��2��6@    5�   .   �� 5�   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+% �� "&   �� 5�   2   $��* �6C   .+�5�   .   �� $+ PART_HorizontalScrollBar��68   $��1��6Z   $, 
Horizontal=�6j   #. z4 6�   #0 z5 6�   #y�z6 5�   8   2 5�   8   �� *HorizontalOffset+$3 OneWay=�% �� *TemplatedParent+5�   +   5�   '   5�   #   .��5�      �� # z 6-   5�      5�      5�      5�      7 5�   
   P� 5�      8 �@��
IsGrouping9 . . �5�      ��  � �� .  5�      5�      5     �� 5     "  6   .�5  
   ��  � ��
 �����5     �� 5  
    � �� 5     �� 5	     "	  6"   .R�5
     �� $ border��6   
������ $��1q�6G   
������  . 
 5       5     FocusStates��. 5  "     	Unfocused��6.     5  "   Focused��6.   5       5     CurrentStates��. 5  "     Regular��6.     5  "   Current��. 5  &   �� .��5  *   �� $ border��6G   $2 ,(Border.BorderBrush).(SolidColorBrush.Color)�6f   .g�5  .     $ 0��6B   8   6N   5  +   5  '   5  #   5     ._�5     �� # z 6+   5     5     5     5(     �� 5+     " 	 6   .�5,  
   ��  � �� 5-     �� 5.     # � 	 6"   50     �� 51  
    �@J�SnapsToDevicePixels . 52     �� 53  
    �@=�ErrorTemplate 54     Z� �� 55  
    �@	 ValidationErrorTemplate 56     �� 57     .R�58     �� 
���  ��$��2,0,0,0q�59  
   $��!��5:  
   $��Center=�5;  
   5<     5>     �� 5?  
    � �� 5@     �� 5A     "	 	 6"   .R�5B     �� $ 
DGR_Border��6   #��z��62   #��z��6^    . ��5C     �� 5D     $  0.5,1&�62   $ 0.5,0&�6C   .��5E  "   � $ Transparent��6/   $ 0Q�6C   � 5F  "   # � 	 6/   $ 1Q�6[   5G     
 5I       5J     CommonStates��. 5K  "     
Normal��6.     5P  "   Normal_AlternatingRow��. 5Q  &   �� .��5R  *   �� $ 
DGR_Border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�6j   .g�5S  .     $ 0��6B   8   6N   5T  +   �� 5U  *   $ 
DGR_Border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6j   .g�5V  .     $ 0��6B   8  6N   5W  +   5X  '   5Y  #     5b  "   Normal_Selected��. 5c  &   �� .��5d  *   �� $ 
DGR_Border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�6j   .g�5e  .     $ 0��6B   8  6O   5f  +   �� 5g  *   $ 
DGR_Border��6G   $O I(Panel.Background). (GradientBrush.GradientStops)[1].(GradientStop.Color)�6j   .g�5h  .     $ 0��6B   8  6O   5i  +   5j  '   5k  #     5n  "   Unfocused_Selected��. 5o  &   �� .��5p  *   �� $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[0].(GradientStop.Color)�5q  '   .g�5s  .     $ 0��6B   8  5t      5u  +   �� 5v  *   $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[1].(GradientStop.Color)�5w  '   .g�5y  .     $ 0��6B   8  5z      5{  +   5|  '   5}  #     5  "   Normal_Editing��. 5�  &   �� .��5�  *   �� $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[0].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8  5�      5�  +   �� 5�  *   $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[1].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8  5�      5�  +   5�  '   5�  #     5�  "   MouseOver_Editing��. 5�  &   �� .��5�  *   �� $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[0].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8  5�      5�  +   �� 5�  *   $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[1].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8 	 5�      5�  +   5�  '   5�  #     5�  "   MouseOver_Unfocused_Editing��. 5�  &   �� .��5�  *   �� $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[0].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8 
 5�      5�  +   �� 5�  *   $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[1].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8  5�      5�  +   5�  '   5�  #     5�  "   Unfocused_Editing��. 5�  &   �� .��5�  *   �� $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[0].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8  5�      5�  +   �� 5�  *   $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[1].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8  5�      5�  +   5�  '   5�  #     5�  "   	MouseOver��. 5�  &   �� .��5�  *   �� $ 
DGR_Border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�6j   .g�5�  .     $ 0��6B   8  6N   5�  +   �� 5�  *   $ 
DGR_Border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6k   .g�5�  .     $ 0��6B   8  6N   5�  +   5�  '   5�  #     5�  "   MouseOver_Selected��. 5�  &   �� .��5�  *   �� $ 
DGR_Border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[0].(GradientStop.Color)�6j   .g�5�  .     $ 0��6B   8  6N   5�  +   �� 5�  *   $ 
DGR_Border��6G   $N H(Panel.Background).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6j   .g�5�  .     $ 0��6B   8  6N   5�  +   5�  '   5�  #     5�  "    MouseOver_Unfocused_Selected��. 5�  &   �� .��5�  *   �� $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[0].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8  5�      5�  +   �� 5�  *   $ 
DGR_Border��6G   $[ U(Panel.Background).             (GradientBrush.GradientStops)[1].(GradientStop.Color)�5�  '   .g�5�  .     $ 0��6B   8  5�      5�  +   5�  '   5�  #   5�     ._�5�     
   5�     �� 5�  "   $
��Auto �63   �� 5�  "   $��* �63    5�     �� 5�  "   $��* �60   �� 5�  "   $
��Auto �60   .+�5�       $��1��65   #��z: 6E   # z 6o     5�     $��1��67   $��1��6H   #y�z; 6U   < 5   -   �� *AreRowDetailsFrozen+#" Z= #$ Z> % �� "&     5     $��2��60   $< Vertical=�6A   y�5     �� *HeadersVisibility+#" Z? #$ Z# % �� "&   5     5     5     5     5     �� 5     " n�5     .�5  
   ��  � ��$ 8��5     �� 5  
    � ��
 ���� 5     �� 5  
    �@�Cursor$ SizeNS��5     �� 5  
    � �� 5     �� 5     "	 n�6"   .R�5     �� #��z��6   # z��5     5     5     5     �� 5     "  6   .�5  
   ��  � �� 5      �� 5!     "	  6"   .R�5"     � 
 5#       5(     CommonStates��. 5)  "     
Normal��6.     5*  "   Normal_CurrentRow��6.     5+  "   Unfocused_EditingRow��6.     5,  "   Normal_EditingRow��6.     5-  "   	MouseOver��. 5.  &   �� .��5/  *   �� $ rowHeaderBorder��6G   $e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�50  1   .g�52  .     $ 0��6B   8   53  *   54  +   55  '   56  #     57  "   MouseOver_CurrentRow��6.     58  "   "MouseOver_Unfocused_EditingRow��6.     59  "   MouseOver_EditingRow��6.     5:  "    MouseOver_Unfocused_Selected��6.     5;  "   MouseOver_Selected��6.     5<  "   +'MouseOver_Unfocused_CurrentRow_Selected��6.     5=  "   !MouseOver_CurrentRow_Selected��6.     5>  "   Unfocused_Selected��6.     5?  "   !Unfocused_CurrentRow_Selected��6.     5@  "   Normal_CurrentRow_Selected��6.     5A  "   Normal_Selected��6.   5B     .+�5D     �� $ rowHeaderBorder��6!   $��10��5E     $��1q�5F     ��5G     �� 5H  "   $  0.5,1&�66   $ 0.5,0&�5I  $   .��5J  &   � # �  63   $ 0Q�5K     � 5L  &   # �  63   $ 1Q�5M     5N  #   ��5P     �� 5Q  "   $  0.5,1&�66   $ 0.5,0&�5R  $   .��5S  &   � # � 	 63   $ 0Q�5T     � 5U  &   # � 
 63   $ 1Q�5V     5W  #   ._�5Y     �� $@ 
Horizontal=�6)   .+�5Z  "   �� $��Center=�63   # z 5[  !   �� 5\  "    .  ��5]     �� *ValidationErrorTemplate+% �� "& 	 y�5_     �� *(Validation.HasError)+8$  % �� "& 	 5b     5c     n� 5e     $A PART_TopHeaderGripper��6    8�� $	��Top=�5g     n� 5h     $A PART_BottomHeaderGripper��6    8�� $��Bottom=�5j     5k     5l     5n     5o     �� 5r     " n�5s     .�5t  
   ��  � ��$ 8��5u     �� 5v  
    � ��
 ���� 5w     �� 5x  
    �@�Cursor$ SizeWE��5y     �� 5z  
    � �� 5{     �� 5|     "	 n�6"   .R�5}     �� #��z��6   # z��5~     5     5�     5�     �� 5�     "  6   .�5�  
   ��  � ��$ Center=�5�     �� 5�  
    � �� 5�     �� 5�     "	  6"   .R�5�     � .+�5�     �� $ columnHeaderBorder��6!   $��1q�5�     $ 3,0,3,0q�5�     ��5�     �� 5�  "   $  0.5,1&�66   $ 0.5,0&�5�  $   .��5�  &   � # �  63   $ 0Q�5�     � 5�  &   # �  63   $ 1Q�5�     5�  #   ��5�     �� 5�  "   $  0.5,1&�66   $ 0.5,0&�5�  $   .��5�  &   � # � 	 63   $ 0Q�5�     � 5�  &   # � 
 63   $ 1Q�5�     5�  #   ._�5�     �� #��z��6/   # z 5�     #��z��5�     5�     n� 5�     $A PART_LeftHeaderGripper��6    $
��Left=�5�     8��  5�     n� 5�     $A PART_RightHeaderGripper��6    $��Right=�5�     8�� 5�     5�     5�     5�     �� 5�  
    � �� 5�     �� 5�     $  0.5,1&�6&   $ 0.5,0&�5�     .��5�     � # � 	 6#   $ 0Q�5�     � 5�     # � 
 6#   $ 1Q�5�     5�     5�     5�     �� 5�     "  6   .�5�  
   ��  � �� 5�     �� 5�     "	  6"   .R�5�     � .+�5�       $B PART_FillerColumnHeader��6/   C .  5�  !   �� 5�     5�     5�     5�     5�     5�     