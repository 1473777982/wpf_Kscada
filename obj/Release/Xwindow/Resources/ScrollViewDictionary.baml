   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ScrollBarThumb  �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ! HorizontalScrollBarPageButton  VerticalScrollBarPageButton   VerticalScrollBarPageButton2  for_scrollbar �Name J� SnapsToDevicePixels � RowDefinitions �� PageUpCommand	 ]�Name
 J� IsMouseOver ]� DecreaseRepeatButton ]� IncreaseRepeatButton �� PageDownCommand ]� Thumb �� Triggers P� Property P� Value �� PageLeftCommand �� PageRightCommand  for_scrollviewer � Cursor ��Name �� ScrollableHeight � Maximum �� Orientation �� ViewportHeight �� ViewportSize �� VerticalOffset � Value* �� #ComputedVerticalScrollBarVisibility �� ScrollableWidth �� HorizontalOffset  �� ViewportWidth,! �� %ComputedHorizontalScrollBarVisibility" �� Triggers# :� RoutedEvent$ �� 
TargetName% �� TargetProperty	& W� To' g� Duration( g� 	BeginTime) :� 
SourceName�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     .sys&clr-namespace:System;assembly=mscorlib  %�  &	        &	 �     &	 [    &	 )    &	     7  7  7  7   7  7  7  7   &	 &    7  7  �� "  n�.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � �� �� " n�.R�� .+��� ���#646465$ 3���� "  ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$	 0.2Q���  � �� �� " ��.R��� #��z��#��z��#��z���� "  ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$	 0.2Q���  � �� �� " ��.R��� #��z��#��z��#��z���� "  ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  �@J�Opacity$ 0Q���  � �� �� " ��.R��� ���	#90000000$��0��$��0���� "  ��.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��
 ���� ��  � ��$ 0,1,1,6q���  � ��$ 10����  � ��$ 5����  �@J�Opacity$	 0.2Q���  � �� �� " ��.R�� $ Bg�� .  �� $
��auto ��� $��* ��� $
��auto �.+��� $��0��8��  #��Z ]� $	 
PART_Track��$��1��#{�z
 ��.  �� 8�� #��Z  �� 8�� #��Z  n� 8�� �� $��2��8�� #��Z  P�  �@��Orientation$ 
Horizontal=�. ���  � ��
 ���� ��  � ��$ 1,0,6,1q���  � ��$ 5����  � ��$ 5����  � ��$
 Auto����  �@J�Opacity$	 0.2Q���  � �� �� " ��.R�� $ Bg�� . .+�]� $	 
PART_Track��#{�z
  �� 8�� #��Z  �� 8�� #��Z  n� 8�� �� "  ��.���  � ��
 �������  � ��$ 0q���  � �� �� " ��.R��� #��z��#��z�� . ._�� #��z��.+��� # z #��z��#��z���� $ PART_VerticalScrollBar��$��Right=�# z $ Vertical=�8��  # z # z #y�z �� $ PART_HorizontalScrollBar��# z $ 
Horizontal=�8�� $��Bottom=�# z # z  #y�z! " :� $# ScrollChanged��.[��� .���� .��W� $$ PART_VerticalScrollBar��$% Opacity�$	& 0.8X�$' 0:0:1D�W� $$ PART_VerticalScrollBar��$% Opacity�$	& 0.2X�$' 0:0:1D�$( 0:0:1X�W� $$ PART_HorizontalScrollBar��$% Opacity�$	& 0.8X�$' 0:0:1D�W� $$ PART_HorizontalScrollBar��$% Opacity�$	& 0.2X�$' 0:0:1D�$( 0:0:1X�:� $# 
MouseEnter��$) PART_VerticalScrollBar��.[��� .���� .��W� $$ PART_VerticalScrollBar��$% Opacity�$	& 0.8X�$' 0:0:0.7D�:� $# 
MouseLeave��$) PART_VerticalScrollBar��.[��� .���� .��W� $$ PART_VerticalScrollBar��$% Opacity�$	& 0.2X�$' 0:0:0.7D�:� $# 
MouseEnter��$) PART_HorizontalScrollBar��.[��� .���� .��W� $$ PART_HorizontalScrollBar��$% Opacity�$	& 0.8X�$' 0:0:0.7D�:� $# 
MouseLeave��$) PART_HorizontalScrollBar��.[��� .���� .��W� $$ PART_HorizontalScrollBar��$% Opacity�$	& 0.2X�$' 0:0:0.7D�