   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   bool2VisibilityConverter  WindowColor  ContentAreaColorLight  ContentAreaColorDark  DisabledControlLightColor  DisabledControlDarkColor  DisabledForegroundColor  SelectedBackgroundColor  SelectedUnfocusedColor 	 ControlLightColor 
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
?   Row  RowHeaderGripperStyle@ �� OrientationA n�Name  ColumnHeaderGripperStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeaderB  NameC J� IsHitTestVisible�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      :x,http://schemas.microsoft.com/winfx/2006/xaml      %�'  &	        &	      &	      &	 +     &	 >     &	 Q     &	 d     &	 w     &	 �     &		 �     &	
 �     &	 �     &	 �     &	 �     &	 �     &	     &	 @    &	 S    &	 f    &	 y    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	 �    &	 	    &	     &	 z    (��     $" DataGridSelectAllButtonStyle��"   )7  7  '   P    '  �    7  '	  �    7  7  7 
 7  7 
 7  7 
 7  7 
 7  7 
 7  7 
 7  7 
 7  7  7  7  7  &	 �    '  $    7  7   7  7  &	 �"    '  j#    7  7  '  _%    �� ��	#FFE8EDF9����	#FFC5CBF9����	#FF7381F9����	#FFE8EDF9����	#FFC5CBF9����	#FF888888����	#FFC5CBF9����	#FFDDDDDD����	White����	#FF7381F9����	#FF211AA9����	#FF3843C4����	#FF211AA9����	#FF444444����/+sc#1, 0.004391443, 0.002428215, 0.242281124����	#FFCCCCCC����	#FF888888����	#FF444444����	#FF888888����	#FF444444����	#FFAAAAAA����	#FF888888����	Black����	#FFC5CBF9����	Black����	#FFC5CBF9����	#FF3843C4���� $  0.5,1&�$ 0.5,0&�.��� # � 	 $ 0Q�� # � 
 $	 0.5Q�� # � 	 $ 1Q��� $	 0,0&�$	  1,0&���� � $ 	#000000FF��$ 0Q�� $ 	#600000FF��$	 0.4Q�� $ 	#600000FF��$	 0.6Q�� $ 	#000000FF��$ 1Q��� " ��.���  � �� �� "	 ��.R�� 
   CommonStates��.   
Normal��  	MouseOver��. �� .���� $ Border��$_ Y(Shape.Fill).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8     Pressed��. �� .���� $ Border��$_ Y(Shape.Fill).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Disabled��. �� .��T� $ (UIElement.Visibility)�$ Arrow��..�i� $ 0��# Z .+��� $ Border�� . ���� $  0.5,1&�$ 0.5,0&�.��� # �  $ 0Q�� # �  $ 1Q����� $  0.5,1&�$ 0.5,0&�.��� # � 	 $ 0Q�� # � 
 $ 1Q�� $ Arrow��$��Right=�$��8,8,3,3q�$
 0.15Q� �   ��� ��� ��� ��� $ Uniform=�$��Bottom=����� # �  �� "   .���  � ��# � ����  � �� �� $  0.5,1&�$ 0.5,0&�.��� # �  $ 0Q�� # �  $ 1Q���  � ��$ 1q���   �@  RowDetailsVisibilityMode$ VisibleWhenSelected=���  � �� . ��  �@��PanningMode$
 Both=���  �@��IsFlicksEnabled .  ��  � �� �� "	   .R��� $ border�� . #��z��#��z��# z������ # � 	 
   CommonStates��.   Disabled��. �� .���� $ border��$G A(Panel.Background).                       (SolidColorBrush.Color)�.g�  $ 0��# � 	   
Normal��._��� $ DG_ScrollViewer��|�.  
���   ����� "	 ��.R��  �� $
��Auto ��� $��* ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto �.+��� |�.  #��Z  ��C� *�� $" DataGridSelectAllButtonStyle��"   +y��� *HeadersVisibility+#" Z! #$ Z# % �� "&   ���� *CellsPanelHorizontalOffset+% �� "&     $!' PART_ColumnHeadersPresenter��$��1��y��� *HeadersVisibility+#" Z( #$ Z# % �� "&   �� $!) PART_ScrollContentPresenter��$��2��$��1��#* z���� $+ PART_VerticalScrollBar��$��2��$��1��$, Vertical=�#. z- #0 z/ #y�z1 2 �� *VerticalOffset+$3 OneWay=�% �� *TemplatedParent+� $��1��$��2�� �� ���� **(NonFrozenColumnsViewportHorizontalOffset+% �� "&   �� $��* �.+��� $+ PART_HorizontalScrollBar��$��1��$, 
Horizontal=�#. z4 #0 z5 #y�z6 2 �� *HorizontalOffset+$3 OneWay=�% �� *TemplatedParent+.���� # z 7 P� 8 �@��
IsGrouping9 . . ���  � �� .  �� "  .���  � �� �� "	  .R��� $ border��
������ $��1q�
������  . 
   FocusStates��.   	Unfocused��  Focused��  CurrentStates��.   Regular��  Current��. �� .���� $ border��$I C(Border.BorderBrush).                       (SolidColorBrush.Color)�.g�  $ 0��8   ._��� # z �� " 	 .���  � �� �� # � 	 ��  �@J�SnapsToDevicePixels . ��  �@=�ErrorTemplate Z� ��  �@	 ValidationErrorTemplate �� .R��� 
���  ��$��2,0,0,0q�$��!��$��Center=���  � �� �� "	 	 .R��� $ 
DGR_Border��#��z��#��z�� . ���� $  0.5,1&�$ 0.5,0&�.��� $ Transparent��$ 0Q�� # � 	 $ 1Q�
   CommonStates��.   
Normal��  Normal_AlternatingRow��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8   �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Normal_Selected��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Unfocused_Selected��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Normal_Editing��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    MouseOver_Editing��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8 	   MouseOver_Unfocused_Editing��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8 
 �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    Unfocused_Editing��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    	MouseOver��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8    MouseOver_Selected��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8     MouseOver_Unfocused_Selected��. �� .���� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[0].(GradientStop.Color)�.g�  $ 0��8  �� $ 
DGR_Border��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8  ._�
   �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+�  $��1��#��z: # z   $��1��$��1��#y�z; < �� *AreRowDetailsFrozen+#" Z= #$ Z> % �� "&     $��2��$< Vertical=�y��� *HeadersVisibility+#" Z? #$ Z# % �� "&   �� " n�.���  � ��$ 8����  � ��
 ���� ��  �@�Cursor$ SizeNS����  � �� �� "	 n�.R��� #��z��# z���� "  .���  � �� �� "	  .R�� 
   CommonStates��.   
Normal��  Normal_CurrentRow��  Unfocused_EditingRow��  Normal_EditingRow��  	MouseOver��. �� .���� $ rowHeaderBorder��$e _(Panel.Background).                       (GradientBrush.GradientStops)[1].(GradientStop.Color)�.g�  $ 0��8     MouseOver_CurrentRow��  "MouseOver_Unfocused_EditingRow��  MouseOver_EditingRow��   MouseOver_Unfocused_Selected��  MouseOver_Selected��  +'MouseOver_Unfocused_CurrentRow_Selected��  !MouseOver_CurrentRow_Selected��  Unfocused_Selected��  !Unfocused_CurrentRow_Selected��  Normal_CurrentRow_Selected��  Normal_Selected��.+��� $ rowHeaderBorder��$��10��$��1q����� $  0.5,1&�$ 0.5,0&�.��� # �  $ 0Q�� # �  $ 1Q����� $  0.5,1&�$ 0.5,0&�.��� # � 	 $ 0Q�� # � 
 $ 1Q�._��� $@ 
Horizontal=�.+��� $��Center=�# z ��  .  ���� *ValidationErrorTemplate+% �� "& 	 y��� *(Validation.HasError)+8$  % �� "& 	 n� $A PART_TopHeaderGripper��8�� $	��Top=�n� $A PART_BottomHeaderGripper��8�� $��Bottom=��� " n�.���  � ��$ 8����  � ��
 ���� ��  �@�Cursor$ SizeWE����  � �� �� "	 n�.R��� #��z��# z���� "  .���  � ��$ Center=���  � �� �� "	  .R�� .+��� $ columnHeaderBorder��$��1q�$ 3,0,3,0q����� $  0.5,1&�$ 0.5,0&�.��� # �  $ 0Q�� # �  $ 1Q����� $  0.5,1&�$ 0.5,0&�.��� # � 	 $ 0Q�� # � 
 $ 1Q�._��� #��z��# z #��z��n� $A PART_LeftHeaderGripper��$
��Left=�8��  n� $A PART_RightHeaderGripper��$��Right=�8�� ��  � �� �� $  0.5,1&�$ 0.5,0&�.��� # � 	 $ 0Q�� # � 
 $ 1Q��� "  .���  � �� �� "	  .R�� .+�  $B PART_FillerColumnHeader��C .  �� 